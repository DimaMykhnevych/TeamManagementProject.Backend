using AutoMapper;
using Microsoft.AspNetCore.Http;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1.Responses;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Mapper.Resolvers
{
    public class EventIsMadeByUserResolver
        : IValueResolver<Event, EventsForUserResponse, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly IHttpContextAccessor _accessor;

        public EventIsMadeByUserResolver(IHttpContextAccessor accessor, IIdentityService identityService)
        {
            _accessor = accessor;
            _identityService = identityService;
        }

        public bool Resolve(Event source, EventsForUserResponse destination, bool destMember, ResolutionContext context)
        {
            var currentUser = _identityService.GetAppUserAsync(_accessor.HttpContext.User).Result;
            return currentUser == source.CreatedBy;
        }
    }
}
