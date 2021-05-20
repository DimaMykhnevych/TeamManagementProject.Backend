using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Exceptions;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper,
            UserManager<AppUser> userManager, IIdentityService identityService,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _identityService = identityService;
            _userRepository = userRepository;
        }
        public async Task<AppUser> CreateUserAsync(UserCreateRequest model, string role)
        {
            AppUser existingUser = await _userManager.FindByNameAsync(model.Username);
            if (existingUser != null)
            {
                throw new UsernameAlreadyTakenException();
            }

            AppUser user = _mapper.Map<AppUser>(model);
            user.FirstName = model.Username;
            user.LastName = "CEO";
            user.Position = "CEO";
            IdentityResult addUserResult = await _userManager.CreateAsync(user, model.Password);
            await _identityService.AddToRoleAsync(new Guid(user.Id), role);
            ValidateIdentityResult(addUserResult);
            return await _userManager.FindByNameAsync(user.UserName);
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _userRepository.GetUsers();
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
