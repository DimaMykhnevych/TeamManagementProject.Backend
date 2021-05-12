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

        public CompanyService(IMapper mapper, ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<CompanyCreateResponse> AddCompany(CompanyCreateRequest companyRequest)
        {
            Company company = _mapper.Map<Company>(companyRequest);
            company.Subscription = new Subscription();
            company.Subscription.Transaction = new Transaction();
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
