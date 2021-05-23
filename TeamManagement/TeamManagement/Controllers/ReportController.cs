using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.Authorization;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;
using TeamManagement.Contracts.v1.Responses;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.Controllers
{
    public class ReportController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IGenericRepository<Report> _genericReportRepository;
        private readonly IMapper _mapper;

        public ReportController(IIdentityService identityService, IGenericRepository<Report> genericReportRepository, IMapper mapper)
        {
            _identityService = identityService;
            _genericReportRepository = genericReportRepository;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.Report.BaseWithVersion)]
        [RequireRoles("Administrator")]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportRequest creationRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errors);
            }

            var appUserId = (await _identityService.GetAppUserAsync(this.User)).Id;

            var report = _mapper.Map<Report>(creationRequest);
            report.PublisherId = appUserId;
            report.DateOfPublishing = DateTime.Now;

            if (await _genericReportRepository.CreateAsync(report))
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpGet(ApiRoutes.Report.BaseWithVersion)]
        public async Task<IActionResult> GetReports([FromQuery] GetReportPageRequest request)
        {
            var reports = await _genericReportRepository.GetAsync(includeFunc: reports => reports.Include(report => report.Publisher).ThenInclude(p => p.Team).Include(ev => ev.ReportRecords),
                                                              filter: ev => ev.Publisher.TeamId == (_identityService.GetAppUserTeam(this.User)).Result.Id && ev.DateOfPublishing.Year == request.Date.Year && ev.DateOfPublishing.Month == request.Date.Month && ev.DateOfPublishing.Day == request.Date.Day, orderBy: rep => rep.OrderByDescending(rep => rep.DateOfPublishing)); ;

            var response = _mapper.Map<List<GetReportsResponse>>(reports);

            return Ok(response);
        }
    }
}
