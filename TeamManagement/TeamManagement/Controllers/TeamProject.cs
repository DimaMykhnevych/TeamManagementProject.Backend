using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;

namespace TeamManagement.Controllers
{
    [ApiController]
    public class TeamProject : ControllerBase
    {
        private readonly ITeamProjectService _teamProjectService;

        public TeamProject(ITeamProjectService teamProjectService)
        {
            _teamProjectService = teamProjectService;
        }

        [HttpPost(ApiRoutes.TeamProject.BaseWithVersion)]
        public async Task<IActionResult> CreateTeamProject(TeamProjectCreateRequest teamProjectCreateRequest)
        {
            var teamProject = await _teamProjectService.AddTeamProject(teamProjectCreateRequest);
            return Ok(teamProject);
        }
    }
}
