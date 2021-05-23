using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;

namespace TeamManagement.BusinessLayer.Services.Interfaces
{
    public interface ITeamService
    {
        Task<TeamCreateResponse> CreateTeam(TeamCreateRequest teamCreateRequest, string currentUserName);
    }
}
