using AutoMapper;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.BusinessLayer.Services
{
    public class TeamProjectService : ITeamProjectService
    {
        private readonly IGenericRepository<TeamProject> _teamProjectRepository;
        private readonly IMapper _mapper;

        public TeamProjectService(IGenericRepository<TeamProject> genericRepository, IMapper mapper)
        {
            _mapper = mapper;
            _teamProjectRepository = genericRepository;
        }

        public async Task<TeamProjectCreateResponse> AddTeamProject(TeamProjectCreateRequest teamProjectRequest)
        {
            TeamProject teamProject = _mapper.Map<TeamProject>(teamProjectRequest);
            await _teamProjectRepository.CreateAsync(teamProject);
            return _mapper.Map<TeamProjectCreateResponse>(teamProject);
        }
    }
}
