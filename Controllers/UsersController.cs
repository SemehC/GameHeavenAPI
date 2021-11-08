using GameHeavenAPI.Dtos;
using GameHeavenAPI.Dtos.UserDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories;
using GameHeavenAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUsersRepository repository;

        public UsersController(IUsersRepository repository)
        {
            this.repository = repository;
        }



        [HttpGet]
        public IEnumerable<GetUserDto> GetUsers()
        {
            
            return repository.GetUsers();
        }


        /// <summary>
        /// Method : GET <br/>
        /// Path : users/id <br/>
        /// Get user by it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public async Task<GetUserDto> GetUserAsync(Guid id)
        {
            return await repository.GetUser(id);
        }



        /// <summary>
        /// Method : POST <br/>
        /// Path : users/new <br />
        /// Register a new user to database returning errors if found
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>

        [HttpPost("new")]
        public async Task<ActionResult<ServerResponse<IEnumerable<IdentityError>>>> CreateUserAsync(CreateUserDto userDto)
        {

            ApplicationUser user = new()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                UserName = userDto.UserName,
                Email = userDto.Email,
                Birthday = userDto.Birthday,
                JoinDate = DateTimeOffset.UtcNow,
            };
            var r = await repository.CreateUser(user, userDto.Password);
            return r;
        }


        [HttpPost("signin")]
        public async Task<ActionResult<String>> LoginUser(LoginUserDto loginDetails)
        {
            var resp = await repository.LoginUser(loginDetails);
            return resp;
        }
    }
}
