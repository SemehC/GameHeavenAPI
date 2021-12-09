using GameHeavenAPI.Services;
using GameHeavenAPI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using GameHeavenAPI.Dtos.Requests;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Dtos.UserDtos;
using System.IO;
using System.Web;
using GameHeavenAPI.Dtos;
using GameHeavenAPI.Repositories;
using GameHeavenAPI.Dtos.Responses;

namespace GameHeavenAPI.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Roles.Admin))]
    public class UserSetupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        protected readonly ILogger<UserSetupController> _logger;
        private readonly IPublishersRepository _publishersRepository;
        private readonly IGameRepository _gameRepository;
        public UserSetupController(
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger<UserSetupController> logger, IGameRepository gameRepository, IPublishersRepository publishersRepository)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _gameRepository = gameRepository;
            _publishersRepository = publishersRepository;
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles.Select(role => role.Name));
        }

        [HttpPost]
        [Route("CreateRole/{roleName}")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                //create the roles and seed them to the database
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (roleResult.Succeeded)
                {
                    _logger.LogInformation(1, "Roles Added");
                    return Ok(new { result = $"Role {roleName} added successfully" });
                }
                else
                {
                    _logger.LogInformation(2, "Error");
                    return BadRequest(new { error = $"Issue adding the new {roleName} role" });
                }
            }

            return BadRequest(new { error = "Role already exist" });
        }

        // Get all users
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }
        [HttpGet]
        [Route("GetUser/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (user is not null)
            {
                return Ok(user);
            }
            return BadRequest(new Response
            {
                Success = false,
                Errors = new()
                {
                    "User not found",
                }
            });
        }
        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromForm] UpdateUserDto updateUserDto)
        {
            var userToBeUpdated = _userManager.Users.Where(user => user.Id.Equals(id)).First();
            if (userToBeUpdated is not null)
            {
                userToBeUpdated.UserName = updateUserDto.UserName;
                userToBeUpdated.Email = updateUserDto.Email;
                userToBeUpdated.FacebookLink = updateUserDto.FacebookLink;
                userToBeUpdated.InstagramLink = updateUserDto.InstagramLink;
                userToBeUpdated.TwitterLink = updateUserDto.TwitterLink;
                if (updateUserDto.Password != null)
                {
                    var passwordChanged = await _userManager.ChangePasswordAsync(userToBeUpdated, updateUserDto.OldPassword, updateUserDto.Password);
                    if(!passwordChanged.Succeeded)
                    {
                        return BadRequest(new RegistrationResponseDto
                        {
                            Success = false,
                            Errors = passwordChanged.Errors.Select(x => x.Description).ToList(),
                        });
                    }
                }
                if (updateUserDto.Cover is not null)
                {
                    var path = $"Uploads/Users/Cover/{userToBeUpdated.UserName}";

                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true);
                    }
                    Directory.CreateDirectory(path);
                    var filePath = $"{path}/{updateUserDto.Cover.FileName}";
                    var encodedPath = HttpUtility.UrlEncode(filePath);
                    var coverLink = $"https://localhost:5001/GetImage/{encodedPath}";
                    using var stream = new FileStream(Path.Combine(path, updateUserDto.Cover.FileName), FileMode.Create);
                    await updateUserDto.Cover.CopyToAsync(stream);
                    userToBeUpdated.CoverPath = coverLink;
                }
                if (updateUserDto.ProfilePicture is not null)
                {
                    var path = $"Uploads/Users/ProfilePicture/{userToBeUpdated.UserName}";

                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true);
                    }
                    Directory.CreateDirectory(path);
                    var filePath = $"{path}/{updateUserDto.ProfilePicture.FileName}";
                    var encodedPath = HttpUtility.UrlEncode(filePath);
                    var pictureLink = $"https://localhost:5001/GetImage/{encodedPath}";
                    using var stream = new FileStream(Path.Combine(path, updateUserDto.ProfilePicture.FileName), FileMode.Create);
                    await updateUserDto.ProfilePicture.CopyToAsync(stream);
                    userToBeUpdated.ProfilePicturePath = pictureLink;
                }
                await _userManager.UpdateAsync(userToBeUpdated);
                UserDto userDto = new();

                var publisher = await _publishersRepository.GetPublisherByUserAsync(userToBeUpdated.Id);
                userDto.UserProperties = userToBeUpdated;
                userDto.Publisher = publisher?.AsDto();
                userDto.Roles = await _userManager.GetRolesAsync(userToBeUpdated);
                userDto.OwnedGames = (await _gameRepository.GetUserOwnedGamesAsync(userToBeUpdated)).Select(game => game.AsDto()).ToList();
                return Ok(new RegistrationResponseDto
                {
                    Success = true,
                    Messages = new List<string> { "Profile updated successfully" },
                    User = userDto,
                });
            }
            return BadRequest(new Response
            {
                Success = false,
                Errors = new()
                {
                    "User not found",
                }
            });
        }
        [HttpPut]
        [Route("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var userToBeDeleted = _userManager.Users.Where(user => user.Id.Equals(id)).FirstOrDefault();
            if (userToBeDeleted is not null)
            {
                var result = await _userManager.DeleteAsync(userToBeDeleted);
                return Ok(new Response
                {
                    Success = true,
                    Messages = new List<string>()
                    {
                        "User deleted successfully",
                    }
                });
            }
            return BadRequest(new Response
            {
                Success = false,
                Errors = new()
                {
                    "User not found",
                }
            });
        }


        // Add User to role
        [HttpPost]
        [Route("AddUserToRole/{email}/{roleName}")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);

                if (result.Succeeded)
                {
                    _logger.LogInformation(1, $"User {user.Email} added to the {roleName} role");
                    return Ok(new { result = $"User {user.Email} added to the {roleName} role" });
                }
                else
                {
                    _logger.LogInformation(1, $"Error: Unable to add user {user.Email} to the {roleName} role");
                    return BadRequest(new { error = $"Error: Unable to add user {user.Email} to the {roleName} role" });
                }
            }

            // User doesn't exist
            return BadRequest(new { error = "Unable to find user" });
        }

        // Get specific user role
        [HttpGet]
        [Route("GetUserRoles/{email}")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            // Resolve the user via their email
            var user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {

                // Get the roles for the user
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(roles);

            }
            return BadRequest(new { error = $"User with email {email} not found." });
        }


        [HttpPost]
        [Route("UpdateUserRoles/{email}")]
        public async Task<IActionResult> UpdateUserRoles(string email, UserUpdateRolesDto userUpdateRolesDto)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null && userUpdateRolesDto != null)
            {

                var previousRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in previousRoles)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
                foreach (var role in userUpdateRolesDto.Roles)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
                var msg = new List<string>(){
                        $"User {user.Email} roles updated",
                    };
                return Ok(new Response
                {
                    Messages = msg,
                    Success = true
                });
            }

            // User doesn't exist
            return BadRequest(new { error = "Unable to find user" });
        }

        // Remove User from role
        [HttpPost]
        [Route("RemoveUserFromRole")]
        public async Task<IActionResult> RemoveUserFromRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);

                if (result.Succeeded)
                {
                    _logger.LogInformation(1, $"User {user.Email} removed from the {roleName} role");
                    return Ok(new { result = $"User {user.Email} removed from the {roleName} role" });
                }
                else
                {
                    _logger.LogInformation(1, $"Error: Unable to removed user {user.Email} from the {roleName} role");
                    return BadRequest(new { error = $"Error: Unable to removed user {user.Email} from the {roleName} role" });
                }
            }

            // User doesn't exist
            return BadRequest(new { error = "Unable to find user" });
        }
        // Remove User from role
        [HttpDelete]
        [Route("RemoveRole/{roleName}")]
        public async Task<IActionResult> RemoveRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
                return Ok(new Response
                {
                    Messages = new List<string>()
                    {
                        "Role deleted successfully"
                    },
                    Success = true,

                });
            }
            return NotFound(new Response
            {
                Errors = new List<string>()
                    {
                        "Role not found"
                    },
                Success = false,

            });
        }
    }
}
