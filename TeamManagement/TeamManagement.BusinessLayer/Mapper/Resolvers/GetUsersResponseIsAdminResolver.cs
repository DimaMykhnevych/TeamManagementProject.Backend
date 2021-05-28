using AutoMapper;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1.Responses;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Mapper.Resolvers
{
    public class GetUsersResponseIsAdminResolver : IValueResolver<AppUser, GetUserResponse, bool>
    {
        private readonly IIdentityService _identityService;
        public GetUsersResponseIsAdminResolver(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public bool Resolve(AppUser source, GetUserResponse destination, bool destMember, ResolutionContext context)
        {
            return source.Position == "TeamLead" || source.Position == "ProjectManager" || source.Position == "CEO";
        }
    }
}
