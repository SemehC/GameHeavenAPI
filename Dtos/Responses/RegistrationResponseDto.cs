using GameHeavenAPI.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace GameHeavenAPI.Dtos.Responses
{
    public class RegistrationResponseDto : AuthResult
    {
        public object User { get; set; }
    }
}
