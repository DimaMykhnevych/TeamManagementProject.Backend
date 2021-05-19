using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;

namespace TeamManagement.Controllers
{
    [ApiController]
    [Authorize(Roles = "CEO")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost(ApiRoutes.Project.BaseWithVersion)]
        public async Task<IActionResult> CreateProject(ProjectCreateRequest projectCreateRequest)
        {
            ProjectCreateResponse projectCreateResponse =  await _projectService.CreateProject(projectCreateRequest);
            return Ok(projectCreateResponse);
        }
    }
}
