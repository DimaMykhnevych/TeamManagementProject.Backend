using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Contracts.v1.Responses;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;

namespace TeamManagement.Controllers
{

    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet(ApiRoutes.Company.isAvail)]
        public async Task<IActionResult> IsAvailable()
        {
            (bool, string) didPay = await _companyService.DidCompanyPay(this.User);

            if (didPay.Item1)
            {
                return Ok();
            }

            return BadRequest(didPay.Item2);
        }

        [HttpPost(ApiRoutes.Company.BaseWithVersion)]
        public async Task<IActionResult> AddCompany([FromBody] CompanyCreateRequest company)
        {
            CompanyCreateResponse companyResponse = await _companyService.AddCompany(company);
            return Ok(companyResponse);
        }

        [HttpGet(ApiRoutes.Company.GetById)]
        public async Task<IActionResult> GetCompanyById(Guid id)
        {
            CompanyGetByIdResponse response = await _companyService.GetCompanyById(id);
            return Ok(response);
        }
    }
}
