using GameHeavenAPI.Dtos;
using GameHeavenAPI.Dtos.UserDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories
{
    public interface IUsersRepository
    {
        Task<ApplicationUser> GetUser(int id);
        IEnumerable<ApplicationUser> GetUsers();
        Task<string> LoginUser(LoginUserDto loginDetails);
        Task<ServerResponse<IEnumerable<IdentityError>>> CreateUser(ApplicationUser user, string password);
       
    }
}