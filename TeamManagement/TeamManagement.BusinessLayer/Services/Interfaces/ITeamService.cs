using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;

namespace TeamManagement.BusinessLayer.Services.Interfaces
{
    public interface ITeamService
    {
        Task<TeamCreateResponse> CreateTeam(TeamCreateRequest teamCreateRequest, string currentUserName);
        Task<IEnumerable<TeamGetResponse>> GetTeams(string currentUserName);
        Task<TeamGetResponse> GetTeamById(Guid id);
        void UpdateTeam(TeamCreateRequest teamCreateRequest);
    }
}
