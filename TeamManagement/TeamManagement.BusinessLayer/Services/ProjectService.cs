using AutoMapper;
using System;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.BusinessLayer.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IGenericRepository<Project> _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IGenericRepository<Project> genericRepository, IMapper mapper)
        {
            _projectRepository = genericRepository;
            _mapper = mapper;
        }
        public async Task<ProjectCreateResponse> CreateProject(ProjectCreateRequest projectCreateRequest)
        {
            Project projectToAdd = _mapper.Map<Project>(projectCreateRequest);
            //projectToAdd.CompanyId = new Guid("5FFAEE79-E542-42F8-1021-08D91B597D4E");
            await _projectRepository.CreateAsync(projectToAdd);
            return _mapper.Map<ProjectCreateResponse>(projectToAdd);
        }
    }
}
