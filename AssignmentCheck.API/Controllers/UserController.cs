using AssignmentCheck.Api.Helpers;
using AssignmentCheck.Domain.Configurations;
using AssignmentCheck.Domain.Enums;
using AssignmentCheck.Service.DTOs;
using AssignmentCheck.Service.Helpers;
using AssignmentCheck.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AssignmentCheck.Api.Controllers
{
    [ApiController(), Route("[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPut, Authorize(Roles = CustomRoles.USER_ROLE)]
        public async ValueTask<IActionResult> UpdateAsync( [Required] string password, [FromQuery] UserForUpdateDTO userForUpdateDTO)
            => Ok(await userService.UpdateAsync(password, userForUpdateDTO));

        [HttpPatch("Password"), Authorize(Roles = CustomRoles.USER_ROLE)]    
        public async ValueTask<IActionResult> ChangePasswordAsync(string oldPassword, string newPassword)
            => Ok(await userService.ChangePasswordAsync(oldPassword, newPassword));

        [HttpGet("{id}/Admin"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> GetAsync([FromRoute] Guid id)
            => Ok(await userService.GetAsync(u => u.Id == id));

        /*[HttpPatch("{id}"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> ChangeRoleAsync(Guid? id, UserRole userRole)
        {
            if (!id.HasValue)
                id = Guid.NewGuid();

            return Ok(await userService.ChangeRoleAsync(id.Value, userRole));
        }*/

        /*[HttpGet, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @object)
            => Ok(await userService.GetAllAsync(@object));*/

        /*[HttpGet("User"), Authorize(Roles = CustomRoles.USER_ROLE)]
        public async ValueTask<IActionResult> GetAsync()
            => Ok(await userService.GetAsync(u => u.Id == HttpContextHelper.UserId));*/
    }
}
