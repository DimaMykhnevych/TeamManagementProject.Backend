using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Exceptions;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.DataLayer.Data;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.BusinessLayer.Services
{
    public class EmployeeRegistrationService : IEmployeeRegistrationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private AppDbContext _context;
        private IUserRepository _userRepository;

        public EmployeeRegistrationService(UserManager<AppUser> userManager, 
            IIdentityService identityService, IMapper mapper, AppDbContext appDbContext, IUserRepository userRepository)
        {
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
            _context = appDbContext;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<AppUser>> GetEmployees(string currentUserName)
        {
            AppUser user = await _userRepository.GetUserWithCompany(currentUserName);
            List<AppUser> userWithComp = await _userRepository.GetUsersWithCompanies();
            return userWithComp
                .Where(u => u.TeamId == null && u.Position != "CEO");
        }

        public async Task<IEnumerable<AppUser>> GetAllEmployeesExceptCeo(string currentUserName)
        {
            AppUser user = await _userRepository.GetUserWithCompany(currentUserName);
            List<AppUser> userWithComp = await _userRepository.GetUsersWithCompanies();
            return userWithComp.Where(u => u.Position != "CEO");
        }

        public async Task<AppUser> RegisterEmployee(EmployeeRegistrationRequest employee, string currentUserName)
        {
            AppUser existingUser = await _userManager.FindByEmailAsync(employee.Email);
            AppUser current = await _userRepository.GetUserWithCompany(currentUserName);
            if (existingUser != null)
            {
                throw new UsernameAlreadyTakenException();
            }
            AppUser user = _mapper.Map<AppUser>(employee);
            user.UserName = employee.Email;
            //user.Company = current.Company;
            //user.Company.CeoId = current.Id;
            IdentityResult addUserResult = await _userManager.CreateAsync(user, employee.Password);
            await _identityService.AddToRoleAsync(new Guid(user.Id), user.Position);
            ValidateIdentityResult(addUserResult);
            return await _userManager.FindByNameAsync(user.UserName);
        }

        public async Task<AppUser> UpdateEmployee(EmployeeUpdateRequest employeeToUpdate)
        {
            AppUser user = await _userManager.FindByIdAsync(employeeToUpdate.Id);
            user.FirstName = employeeToUpdate.FirstName;
            user.LastName = employeeToUpdate.LastName;
            user.Email = employeeToUpdate.Email;
            user.DateOfBirth = employeeToUpdate.DateOfBirth;
            user.UserName = employeeToUpdate.Email;
            if(user.Position == null && !String.IsNullOrEmpty(employeeToUpdate.Position))
            {
                user.Position = employeeToUpdate.Position;
                await _identityService.AddToRoleAsync(new Guid(user.Id), user.Position);
            }
            else if(user.Position != employeeToUpdate.Position)
            {
                await _userManager.RemoveFromRoleAsync(user, user.Position);
                user.Position = employeeToUpdate.Position;
                await _identityService.AddToRoleAsync(new Guid(user.Id), user.Position);
            }
           
            
            await _userManager.UpdateAsync(user);
            return user;
        }

        public async Task<bool> DeleteEmployee(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return true;
            }
            return false;
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
