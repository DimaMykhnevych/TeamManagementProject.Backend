using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;
using TeamManagement.DataLayer.Data;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.Controllers
{
    [ApiController]
    [Authorize(Roles = "CEO")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly AppDbContext _context;
        private readonly IGenericRepository<Team> _teamRepository;

        public TeamController(ITeamService teamService, IGenericRepository<Team> genericRepository, AppDbContext appDbContext)
        {
            _teamService = teamService;
            _context = appDbContext;
            _teamRepository = genericRepository;
        }

        [HttpPost(ApiRoutes.Team.BaseWithVersion)]
        public async Task<IActionResult> CreateTeam(TeamCreateRequest teamCreateRequest)
        {
            TeamCreateResponse teamCreateResponse = await _teamService.CreateTeam(teamCreateRequest, User.Identity.Name);
            return Ok(teamCreateResponse);
        }

        [HttpPut(ApiRoutes.Team.BaseWithVersion)]
        public async Task<IActionResult> UpdateTeam(TeamCreateRequest teamCreateRequest)
        {
            Team team = await _teamRepository.GetByIdAsync(teamCreateRequest.Id, includeFunc: teams => teams.Include(team => team.Members));
            team.TeamName = teamCreateRequest.TeamName;

            for (int i = team.Members.Count - 1; i >= 0; i--)
            {
                if (!teamCreateRequest.Members.Any(opt => opt.Id == team.Members[i].Id))
                {
                    team.Members.RemoveAt(i);
                }
            }

            foreach (var member in teamCreateRequest.Members)
            {
                if (!team.Members.Any(mem => mem.Id == member.Id))
                {
                    _context.Entry(member).State = EntityState.Modified;
                    team.Members.Add(member);
                }
            }

            if (await _teamRepository.UpdateAsync(team))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet(ApiRoutes.Team.BaseWithVersion)]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await _teamService.GetTeams();
            return Ok(teams);
        }

        [HttpGet(ApiRoutes.Team.GetById)]
        public async Task<IActionResult> GetTeam([FromQuery] Guid id)
        {
            var team = await _teamService.GetTeamById(id);
            return Ok(team);
        }
    }
}
