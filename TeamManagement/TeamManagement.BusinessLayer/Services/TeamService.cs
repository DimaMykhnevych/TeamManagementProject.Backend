using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.BusinessLayer.Services
{
    public class TeamService : ITeamService
    {
        private readonly IGenericRepository<Team> _teamRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;

        public TeamService(IGenericRepository<Team> genericRepository, IMapper mapper, 
            UserManager<AppUser> userManager, IUserRepository userRepository)
        {
            _teamRepository = genericRepository;
            _mapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public async Task<TeamCreateResponse> CreateTeam(TeamCreateRequest teamCreateRequest, string currentUserName)
        {
            Team team = _mapper.Map<Team>(teamCreateRequest);
            List<AppUser> members = teamCreateRequest.Members;

            AppUser currentUserWithCompany = await _userRepository.GetUserWithCompany(currentUserName);
            team.CompanyId = currentUserWithCompany.Company.Id;

            team.Members = null;
            await _teamRepository.CreateAsync(team);

            foreach (var user in members)
            {
                AppUser us = await  _userManager.FindByIdAsync(user.Id);
                us.TeamId = team.Id;
                await _userManager.UpdateAsync(us);
            }
            return _mapper.Map<TeamCreateResponse>(team);
        }

        public async Task<IEnumerable<TeamGetResponse>> GetTeams()
        {
            IEnumerable<Team> teams = await _teamRepository.GetAsync();
            return _mapper.Map<IEnumerable<TeamGetResponse>>(teams);
        }
    }
}
