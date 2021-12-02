using GameHeavenAPI.Configuration;
using GameHeavenAPI.Dtos.Requests;
using GameHeavenAPI.Dtos.Responses;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GameHeavenAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        protected readonly ILogger<AuthManagementController> _logger;
        private readonly JwtConfig _jwtConfig;

        public AuthManagementController(UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor, RoleManager<IdentityRole> roleManager, ILogger<AuthManagementController> logger)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _roleManager = roleManager;
            _logger = logger;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto userLoginRequestDto)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(userLoginRequestDto.Email);
                if (existingUser is null)
                {
                    return BadRequest(new RegistrationResponseDto
                    {
                        Success = false,
                        Errors = new List<string>
                        {
                            "Invalid login request.",
                        },
                    });
                }
                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, userLoginRequestDto.Password);
                if (!isCorrect)
                {
                    return BadRequest(new RegistrationResponseDto
                    {
                        Success = false,
                        Errors = new List<string>
                        {
                            "Invalid login request.",
                        },
                    });
                }
                else
                {
                    bool emailConfirmed = await _userManager.IsEmailConfirmedAsync(existingUser);
                    if (!emailConfirmed)
                    {
                        return BadRequest(new RegistrationResponseDto
                        {
                            Success = false,
                            Errors = new List<string>
                        {
                            "Email not confirmed yet.",
                        },
                        });
                    }
                    var jwtToken = await GenerateJwtTokenAsync(existingUser);
                    var usr = new User();
                    usr.UserProperties = existingUser;
                    usr.Roles = await _userManager.GetRolesAsync(existingUser);
                    return Ok(new RegistrationResponseDto
                    {
                        Success = true,
                        Token = jwtToken,
                        Messages= new List<string>{ "Welcome " + existingUser.UserName },
                        User = usr,
                    });

                }

            }
            return BadRequest(new RegistrationResponseDto
            {
                Success = false,
                Errors = new List<string>
                        {
                            "Invalid payload",
                        },
            });
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistrationDto)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(userRegistrationDto.Email);
                if (existingUser is not null)
                {
                    return BadRequest(new RegistrationResponseDto
                    {
                        Success = false,
                        Errors = new List<string>
                        {
                            "Email already in use.",
                        },
                    });
                }
                var newUser = new IdentityUser()
                {
                    Email = userRegistrationDto.Email,
                    UserName = userRegistrationDto.UserName,
                    EmailConfirmed = false,


                };
                var isCreated = await _userManager.CreateAsync(newUser, userRegistrationDto.Password);
                if (isCreated.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    var encodedToken = HttpUtility.UrlEncode(token);
                    var confirmationLink = userRegistrationDto.Config + $"?token={encodedToken}&email={newUser.Email}";
                    EmailHelper emailHelper = new EmailHelper();
                    var message = "<h1>Confirm your mail </h1>" +
                                "<p>Use this link to confirm your email.</p>" +
                                confirmationLink +
                                "<br/>" +
                                "Thank you for registring in our platform."
                                ;
                    bool emailResponse = emailHelper.SendEmail(newUser.Email, confirmationLink, message);
                    //Ensures every user gets atleast one role (User in this case)
                    await _userManager.AddToRoleAsync(newUser, Roles.User.ToString());
                    if (emailResponse)
                        return Ok(new RegistrationResponseDto
                        {
                            Success = true,
                            Messages = new List<string> { "Registration successfull, please verify email." },
                        });
                    else
                    {
                        return BadRequest(new RegistrationResponseDto
                        {
                            Success = false,
                            Errors = new List<string>
                            {
                                "Registration failed from our end.."
                            },
                        });
                    }

                }
                else
                {
                    return BadRequest(new RegistrationResponseDto
                    {
                        Success = false,
                        Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                    });
                }
            }
            return BadRequest(new RegistrationResponseDto
            {
                Success = false,
                Errors = new List<string>
                {
                    "Invalid payload",

                },
            });
        }
        [HttpGet]
        [Route("Confirm-Email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(new RegistrationResponseDto
                {
                    Success = false,
                    Errors = new List<string>
                            {
                                "Email Confirmation failed.."
                            },
                });
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok(new RegistrationResponseDto
                {
                    Success = true,
                    Messages = new List<string> { "Email confirmation successfull." },
                });
            }
            var errors = result.Errors.Select(err => err.Code).ToList();
            errors.Add("Registration failed for these reasons:");
            return BadRequest(new RegistrationResponseDto
            {
                Success = false,
                Errors = errors,
                            
            });;
        }
        private async Task<string> GenerateJwtTokenAsync(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var claims = await GetValidClaims(user);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }

        private async Task<List<Claim>> GetValidClaims(IdentityUser user)
        {
            IdentityOptions _options = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
                new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName),
            };
            //getting the claims that we have assigned to the user
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            //get the user roles and add it to the claims
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (Claim roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }
            return claims;
        }
        [HttpPost]
        [Route("Forgot-Password")]
        public async Task<IActionResult> ForgotPassword([FromBody] UserForgotPasswordRequestDto userForgotPasswordRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(userForgotPasswordRequestDto.Email);
            if (user == null)
                return BadRequest(new PasswordRecoveryResponseDto
                {
                    Success = false,
                    Errors = new List<string>
                            {
                                "Email Not Found."
                            },
                });
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);
            _logger.LogInformation($"Token : {token}");
            _logger.LogInformation($"Encoded : {encodedToken}");
            var resetPasswordLink = userForgotPasswordRequestDto.Config + $"?token={encodedToken}&email={user.Email}";
            EmailHelper emailHelper = new EmailHelper();
            var message = "<h1>Reset your password </h1>" +
            "<p>Use this link to reset your password.</p>" +
            resetPasswordLink +
            "<br/>"
            ;
            bool emailResponse = emailHelper.SendEmail(user.Email, resetPasswordLink, message, "Reset your password");

            if (emailResponse)
                return Ok(new RegistrationResponseDto
                {
                    Success = true,
                    Messages = new List<string> { "Password reset email was sent successfully, please verify email." },
                });
            else
            {
                return BadRequest(new RegistrationResponseDto
                {
                    Success = false,
                    Errors = new List<string>
                            {
                                "Password reset failed from our end.."
                            },
                });
            }
        }
        [HttpPost]
        [Route("Reset-Password")]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordRequestDto userResetPasswordRequestDto)
        {
            //var decodedToken = HttpUtility.UrlDecode(userResetPasswordRequestDto.Token);
            //_logger.LogInformation("Decoded : " + decodedToken);
            var user = await _userManager.FindByEmailAsync(userResetPasswordRequestDto.Email);
            if (user == null)
            {
                return BadRequest(new PasswordRecoveryResponseDto
                {
                    Success = false,
                    Errors = new List<string>
                            {
                                "Email Not Found."
                            },
                });
            }

            var result = await _userManager.ResetPasswordAsync(user, userResetPasswordRequestDto.Token, userResetPasswordRequestDto.Password);
            if (result.Succeeded)
            {
                return Ok(new PasswordRecoveryResponseDto
                {
                    Success = true,
                    Messages = new List<string> { "Password has been reset successfully." },
                });
            }
            return BadRequest(new PasswordRecoveryResponseDto
            {
                Success = false,
                Errors = new List<string>
                            {
                                $"Password reset failed.. {result.Errors.First().Code}"
                            },
            });
        }
    }
}
