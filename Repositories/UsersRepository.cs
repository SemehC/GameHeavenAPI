using GameHeavenAPI.Dtos;
using GameHeavenAPI.Dtos.UserDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories
{
    public class UsersRepository : IUsersRepository
    {

        private readonly ApplicationDbContext AppDbContext;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;



        public UsersRepository(ApplicationDbContext appDbContext, UserManager<ApplicationUser> um, SignInManager<ApplicationUser> sm, RoleManager<IdentityRole> rm )
        {
            AppDbContext = appDbContext;
            _userManager = um;
            _signInManager = sm;
            _roleManager = rm;
        }

       
       public async Task<ApplicationUser> GetUser(int id)
        {
            var res = await _userManager.FindByIdAsync(id.ToString());
            if (res == null)
            {
                return null;
            }
            return res;
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return AppDbContext.Users.AsParallel();
        }

        public async Task<ServerResponse<IEnumerable<IdentityError>>> CreateUser(ApplicationUser user, string password)
        {
            if (!_roleManager.RoleExistsAsync(Helper.roles[0]).GetAwaiter().GetResult())
            {
                Helper.roles.ForEach(s =>
                {
                    _roleManager.CreateAsync(new IdentityRole(s));
                });
                
            }
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Helper.roles[0]);
            }
            var resp = new ServerResponse<IEnumerable<IdentityError>>();
            resp.Success = result.Succeeded;
            resp.Data = result.Errors;
            return resp;
        }

        public async Task<string> LoginUser(LoginUserDto loginDetails)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDetails.email, loginDetails.password, true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return "User logged in";
            }
            if (result.IsNotAllowed) return "Not allowed";
            if (result.RequiresTwoFactor)
            {
                return "Two factor login required";
            }
            if (result.IsLockedOut)
            {
                return ("User account locked out");
            }
            else
            {
                return "Invalid login attempt";
            }
        }

       
    }
}
