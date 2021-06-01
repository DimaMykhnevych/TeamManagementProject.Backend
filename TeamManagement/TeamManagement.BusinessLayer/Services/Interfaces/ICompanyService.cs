using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;

namespace TeamManagement.BusinessLayer.Services.Interfaces
{
    public interface ICompanyService
    {
         Task<CompanyCreateResponse> AddCompany(CompanyCreateRequest companyRequest);
        Task<CompanyGetByIdResponse> GetCompanyById(Guid id);
        Task<(bool, string)> DidCompanyPay(ClaimsPrincipal c);
    }
}
