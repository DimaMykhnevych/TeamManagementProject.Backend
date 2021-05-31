using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;

namespace TeamManagement.BusinessLayer.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectCreateResponse> CreateProject(ProjectCreateRequest projectCreateRequest, string currentUserName);
        Task<IEnumerable<ProjectGetResponse>> GetProjects(string currentUserName);
        Task<IEnumerable<ProjectGetResponse>> GetAllProjects(string currentUserName);
        Task<ProjectGetResponse> UpdateProject(ProjectUpdateRequest projectUpdate);
        Task<bool> DeleteProject(Guid id);
    }
}
