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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public ProjectService(IGenericRepository<Project> genericRepository, IMapper mapper, IUserRepository userRepository)
        {
            _projectRepository = genericRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<ProjectCreateResponse> CreateProject(ProjectCreateRequest projectCreateRequest, string currentUserName)
        {
            Project projectToAdd = _mapper.Map<Project>(projectCreateRequest);
            AppUser currentUserWithCompany = await _userRepository.GetUserWithCompany(currentUserName);
            projectToAdd.CompanyId = currentUserWithCompany.Company.Id;
            await _projectRepository.CreateAsync(projectToAdd);
            return _mapper.Map<ProjectCreateResponse>(projectToAdd);
        }
    }
}
