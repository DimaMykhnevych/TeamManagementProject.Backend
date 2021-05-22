using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Exceptions;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.BusinessLayer.Services
{
    public class EmployeeRegistrationService : IEmployeeRegistrationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public EmployeeRegistrationService(UserManager<AppUser> userManager, 
            IIdentityService identityService, IMapper mapper)
        {
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<AppUser> RegisterEmployee(EmployeeRegistrationRequest employee)
        {
            AppUser existingUser = await _userManager.FindByEmailAsync(employee.Email);
            if (existingUser != null)
            {
                throw new UsernameAlreadyTakenException();
            }
            AppUser user = _mapper.Map<AppUser>(employee);
            user.UserName = employee.FirstName + employee.LastName;
            IdentityResult addUserResult = await _userManager.CreateAsync(user, employee.Password);
            await _identityService.AddToRoleAsync(new Guid(user.Id), user.Position);
            ValidateIdentityResult(addUserResult);
            return await _userManager.FindByNameAsync(user.UserName);
        }

        private void ValidateIdentityResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                String errorsMessage = result.Errors
                                         .Select(er => er.Description)
                                         .Aggregate((i, j) => i + ";" + j);
                throw new Exception(errorsMessage);
            }
        }
    }
}
