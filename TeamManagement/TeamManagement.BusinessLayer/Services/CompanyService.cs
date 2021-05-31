using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IGenericRepository<SubscriptionPlan> _subscriptionPlanRepository;
        private readonly UserManager<AppUser> _userManager;

        public CompanyService(IMapper mapper, ICompanyRepository companyRepository, 
            IUserService userService, IGenericRepository<SubscriptionPlan> genericRepository,
            UserManager<AppUser> userManager)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _userService = userService;
            _subscriptionPlanRepository = genericRepository;
            _userManager = userManager;
        }

        public async Task<CompanyCreateResponse> AddCompany(CompanyCreateRequest companyRequest)
        {
            UserCreateRequest user = new UserCreateRequest();
            user.Email = companyRequest.CeoEmail;
            user.Password = companyRequest.CeoPassword;
            user.FirstName = companyRequest.FirstName;
            user.LastName = companyRequest.LastName;
            AppUser addedUser = await _userService.CreateUserAsync(user, "CEO");

            var subscriptionPlansCollection = await _subscriptionPlanRepository.GetAsync();
            List<SubscriptionPlan> subscriptionPlans = subscriptionPlansCollection.ToList();

            Company company = _mapper.Map<Company>(companyRequest);
            company.Subscription = new Subscription();
            company.Subscription.SubscriptionPlanId = subscriptionPlans[0].Id;
            company.Subscription.Transaction = new Transaction();
            company.CeoId = addedUser.Id;
            company.CEO = addedUser;
            await _companyRepository.Insert(company);
            await _companyRepository.Save();

            AppUser addedCeo = await _userManager.FindByIdAsync(addedUser.Id);
            addedCeo.CompanyId = company.Id;
            await _userManager.UpdateAsync(addedCeo);
            return _mapper.Map<CompanyCreateResponse>(company);
        }

        public async Task<CompanyGetByIdResponse> GetCompanyById(Guid id)
        {
            Company company = await _companyRepository.Get(id);
            return _mapper.Map<CompanyGetByIdResponse>(company);
        }
    }
}
