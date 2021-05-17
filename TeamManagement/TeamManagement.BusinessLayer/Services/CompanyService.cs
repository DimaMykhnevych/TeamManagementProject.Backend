using AutoMapper;
using System;
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

        public CompanyService(IMapper mapper, ICompanyRepository companyRepository, IUserService userService)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<CompanyCreateResponse> AddCompany(CompanyCreateRequest companyRequest)
        {
            UserCreateRequest user = new UserCreateRequest();
            user.Email = companyRequest.CeoEmail;
            user.Password = companyRequest.CeoPassword;
            user.Username = companyRequest.CeoUserName;
            AppUser addedUser = await _userService.CreateUserAsync(user);

            Company company = _mapper.Map<Company>(companyRequest);
            company.Subscription = new Subscription();
            company.Subscription.SubscriptionPlanId = new Guid("83531015-3AA5-47DC-B651-0485708BBF03");
            company.Subscription.Transaction = new Transaction();
            company.CeoId = addedUser.Id;
            company.CEO = addedUser;
            await _companyRepository.Insert(company);
            await _companyRepository.Save();
            return _mapper.Map<CompanyCreateResponse>(company);
        }

        public async Task<CompanyGetByIdResponse> GetCompanyById(Guid id)
        {
            Company company = await _companyRepository.Get(id);
            return _mapper.Map<CompanyGetByIdResponse>(company);
        }
    }
}
