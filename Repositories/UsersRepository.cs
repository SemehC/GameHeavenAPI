using GameHeavenAPI.Dtos;
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

       
       

        public IEnumerable<GetUserDto> GetUsers()
        {
            var result = AppDbContext.Users.AsParallel();
            return result.ToList().Select(x => new GetUserDto
            {
                Birthday = x.Birthday,
                Email = x.Email,
                FirstName = x.FirstName,
                IsActive = x.EmailConfirmed,
                JoinDate = x.JoinDate,
                LastName = x.LastName,
                UserName = x.UserName,
            });
        }

        public async Task<ServerResponse<IEnumerable<IdentityError>>> CreateUser(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            var resp = new ServerResponse<IEnumerable<IdentityError>>();
            resp.Success = result.Succeeded;
            resp.Data = result.Errors;
            return resp;
        }
    }
}
