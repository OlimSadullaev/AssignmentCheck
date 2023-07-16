using AssignmentCheck.Api.Helpers;
using AssignmentCheck.Service.Attributes;
using AssignmentCheck.Service.DTOs;
using AssignmentCheck.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentCheck.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }

        [HttpPost, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> CreateAysnc([FromForm] SubjectForCreationDTO dto) 
            => Ok(await subjectService.CreateAsync(dto));

    }
}
