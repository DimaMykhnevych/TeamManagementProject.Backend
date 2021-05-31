using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<ProjectGetResponse>> GetProjects(string currentUserName)
        {
            AppUser user = await _userRepository.GetUserWithCompany(currentUserName);
            IEnumerable<Project> projects = 
                await _projectRepository.GetAsync((proj) => ((proj.TeamProjects == null 
                || !proj.TeamProjects.Any()) && proj.CompanyId == user.Company.Id), 
                includeFunc: (proj) => proj.Include(p => p.TeamProjects));

            return _mapper.Map<IEnumerable<ProjectGetResponse>>(projects);
        }

        public async Task<IEnumerable<ProjectGetResponse>> GetAllProjects(string currentUserName)
        {
            AppUser user = await _userRepository.GetUserWithCompany(currentUserName);
            IEnumerable<Project> projects = await _projectRepository.GetAsync();
            IEnumerable<Project> projectsForCurrentCompany = projects.ToList().Where(p => p.CompanyId == user.Company.Id);
            return _mapper.Map<IEnumerable<ProjectGetResponse>>(projectsForCurrentCompany);
        }

        public async Task<ProjectGetResponse> UpdateProject(ProjectUpdateRequest projectUpdate)
        {
            Project project = await _projectRepository.GetByIdAsync(projectUpdate.Id);
            project.Name = projectUpdate.Name;
            project.ProjectDescription = projectUpdate.ProjectDescription;
            project.StartDate = projectUpdate.StartDate;
            project.EndDate = projectUpdate.EndDate;
            await _projectRepository.UpdateAsync(project);
            return _mapper.Map<ProjectGetResponse>(project);
        }

        public async Task<bool> DeleteProject(Guid id)
        {
            Project project = await _projectRepository.GetByIdAsync(id);
            if (project != null)
            {
                await _projectRepository.DeleteAsync(project);
                return true;
            }
            return false;
        }
    }
}
