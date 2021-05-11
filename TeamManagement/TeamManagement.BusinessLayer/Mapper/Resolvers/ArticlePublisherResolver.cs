using AutoMapper;
using Microsoft.AspNetCore.Http;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Mapper.Resolvers
{
    public class ArticlePublisherResolver : IValueResolver<object, object, AppUser>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IIdentityService _identityService;

        public ArticlePublisherResolver(IHttpContextAccessor accessor, IIdentityService identityService)
        {
            _accessor = accessor;
            _identityService = identityService;
        }

        public AppUser Resolve(object source, object destination, AppUser destMember, ResolutionContext context)
        {
            return _identityService.GetAppUserAsync(_accessor.HttpContext.User).Result;
        }
    }
}
