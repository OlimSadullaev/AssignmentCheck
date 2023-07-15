using AssignmentCheck.Service.DTOs;
using AssignmentCheck.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentCheck.Api.Controllers
{
    [ApiController, Route("[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            this.authService = authService;
            this.userService = userService;
        }

        /// <summary>
        /// Authorization
        /// </summary>
        /// <param name="userForLoginDTO"></param>
        /// <returns></returns>
        
        [HttpPost("login")]
        public async ValueTask<IActionResult> Login(UserForLoginDTO userForLoginDTO)
        {
            var token = await authService.GenerateTokenAsync(userForLoginDTO.Email, userForLoginDTO.Password);
            return Ok(new
            {
                token 
            });
        }

        [HttpPost("register")]
        public async ValueTask<IActionResult> RegistedAsync(UserForCreationDTO userForCreationDTO)
        {
            return Ok(await userService.CreateAsync(userForCreationDTO));
        }
    }
}
