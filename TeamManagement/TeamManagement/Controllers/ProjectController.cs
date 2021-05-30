using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;

namespace TeamManagement.Controllers
{
    [ApiController]
    [Authorize]
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
            ProjectCreateResponse projectCreateResponse =  
                await _projectService.CreateProject(projectCreateRequest, User.Identity.Name);
            return Ok(projectCreateResponse);
        }

        [HttpGet(ApiRoutes.Project.BaseWithVersion)]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectService.GetProjects();
            return Ok(projects);
        }

        [HttpGet(ApiRoutes.Project.AllProjects)]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjects();
            return Ok(projects);
        }

        [HttpPut(ApiRoutes.Project.BaseWithVersion)]
        public async Task<IActionResult> UpdateProject([FromBody]ProjectUpdateRequest projectUpdate)
        {
            var project = await _projectService.UpdateProject(projectUpdate);
            return Ok(project);
        }

        [HttpDelete(ApiRoutes.Project.Delete)]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            bool res = await _projectService.DeleteProject(id);
            if (res)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
    }
}
