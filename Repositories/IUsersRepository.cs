using GameHeavenAPI.Dtos;
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
        
        IEnumerable<GetUserDto> GetUsers();
         Task<ServerResponse<IEnumerable<IdentityError>>> CreateUser(ApplicationUser user, string password);
       
    }
}