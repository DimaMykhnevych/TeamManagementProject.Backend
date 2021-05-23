using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;

namespace TeamManagement.BusinessLayer.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectCreateResponse> CreateProject(ProjectCreateRequest projectCreateRequest, string currentUserName);
    }
}
