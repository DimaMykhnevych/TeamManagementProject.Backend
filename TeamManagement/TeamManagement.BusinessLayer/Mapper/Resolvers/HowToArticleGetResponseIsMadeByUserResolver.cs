using AutoMapper;
using Microsoft.AspNetCore.Http;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1.Responses;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Mapper.Resolvers
{
    public class HowToArticleGetResponseIsMadeByUserResolver
        : IValueResolver<HowToArticle, HowToArticleGetResponse, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly IHttpContextAccessor _accessor;

        public HowToArticleGetResponseIsMadeByUserResolver(IHttpContextAccessor accessor, IIdentityService identityService)
        {
            _accessor = accessor;
            _identityService = identityService;
        }

        public bool Resolve(HowToArticle source, HowToArticleGetResponse destination, bool destMember, ResolutionContext context)
        {
            var currentUser = _identityService.GetAppUserAsync(_accessor.HttpContext.User).Result;
            return currentUser == source.Publisher;
        }
    }
}
