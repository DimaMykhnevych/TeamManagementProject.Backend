﻿using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.AspNetCore.Mvc;
using TeamManagement.BusinessLayer.Services.Interfaces;

namespace TeamManagement.Authorization
{
    public class RequireRolesFilter : IAuthorizationFilter
    {
        private readonly string[] _roles;
        private readonly IIdentityService _identityService;

        public RequireRolesFilter(string[] roles, IIdentityService identityService)
        {
            _roles = roles;
            _identityService = identityService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var isAuthenticated = user.Identity.IsAuthenticated;
            var isInRole = new List<bool>();

            if (!isAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }

            _roles.ToList()
                  .ForEach(role => isInRole.Add(
                      _identityService.HasRole(user.FindFirstValue(ClaimTypes.Name), role).Result
                  ));

            if (isInRole.Contains(false))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
