using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.Authorization;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;
using TeamManagement.Contracts.v1.Requests;
using TeamManagement.Contracts.v1.Responses;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.Controllers
{
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IGenericRepository<Event> _genericEventRepository;
        private readonly IMapper _mapper;

        public EventsController(IIdentityService identityService, IGenericRepository<Event> genericEventRepository, IMapper mapper)
        {
            _identityService = identityService;
            _genericEventRepository = genericEventRepository;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.Event.BaseWithVersion)]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateRequest creationRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errors);
            }

            var eventt = _mapper.Map<Event>(creationRequest);


            foreach (var attend in creationRequest.Attendies)
            {
                eventt.AppUserEvents.Add(new AppUserEvent { AppUserId = attend, Status = "Unknown" });
            }

            var currUser = await _identityService.GetAppUserAsync(this.User);

            eventt.CreatedById = currUser.Id;
            eventt.CreatedBy = currUser;
            eventt.AppUserEvents.Add(new AppUserEvent { AppUserId = currUser.Id, Status = "Unknown" });

            if (await _genericEventRepository.CreateAsync(eventt))
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpGet(ApiRoutes.Event.BaseWithVersion)]
        public async Task<IActionResult> GetEvents([FromQuery] GetEventsPageRequest request)
        {
            var currUserId = (await _identityService.GetAppUserAsync(this.User)).Id;

            var events = await _genericEventRepository.GetAsync(includeFunc: ev => ev.Include(evv => evv.AppUserEvents).ThenInclude(aue => aue.AppUser), filter: ev => (ev.AppUserEvents.Any(aue => aue.AppUserId == currUserId) || ev.CreatedById == currUserId) && ev.DateTime.Year == request.Date.Year && ev.DateTime.Month == request.Date.Month && ev.DateTime.Day == request.Date.Day, orderBy: rep => rep.OrderByDescending(rep => rep.DateTime));

            var eventsResponse = _mapper.Map<List<EventsForUserResponse>>(events);

            return Ok(eventsResponse);
        }

        [HttpDelete(ApiRoutes.Event.BaseWithVersion)]
        public async Task<IActionResult> DeleteEvent([FromQuery] EventDeleteRequest request)
        {
            if (await _genericEventRepository.DeleteAsync(request.Id))
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpPut(ApiRoutes.Event.ChangeAttendingStatus)]
        public async Task<IActionResult> ChangeAttendingStatus([FromBody] ChangeAttendingStatusRequest request)
        {
            var currUserId = (await _identityService.GetAppUserAsync(this.User)).Id;

            var eventt = await _genericEventRepository.GetByIdAsync(request.Id, includeFunc: ev => ev.Include(ev => ev.AppUserEvents));

            eventt.AppUserEvents.FirstOrDefault(ev => ev.AppUserId == currUserId).Status = request.Status;

            if (await _genericEventRepository.UpdateAsync(eventt))
            {
                return Ok(new AttendiesResponse { Id = currUserId });
            }

            return StatusCode(500);
        }
    }
}
