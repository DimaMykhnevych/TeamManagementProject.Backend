using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;

namespace TeamManagement.Controllers
{
    [ApiController]
    [Authorize(Roles = "CEO")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpPost(ApiRoutes.Team.BaseWithVersion)]
        public async Task<IActionResult> CreateTeam(TeamCreateRequest teamCreateRequest)
        {
            TeamCreateResponse teamCreateResponse = await _teamService.CreateTeam(teamCreateRequest);
            return Ok(teamCreateResponse);
        }
    }
}
