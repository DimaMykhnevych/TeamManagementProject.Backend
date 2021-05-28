using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamManagement.Contracts.v1;
using TeamManagement.Authorization;
using TeamManagement.Contracts.v1.Requests;
using TeamManagement.Contracts.v1.Responses;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.DataLayer.Repositories.Interfaces;
using TeamManagement.DataLayer.Domain.Models;
using System.Collections.Generic;
using TeamManagement.BusinessLayer.Contracts.v1.Requests;

namespace TeamManagement.Controllers
{
    [Authorize]
    public class PollsController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IGenericRepository<Poll> _genericPollRepository;
        private readonly IMapper _mapper;

        public PollsController(IIdentityService identityService, IGenericRepository<Poll> genericPollRepository, IMapper mapper)
        {
            _identityService = identityService;
            _genericPollRepository = genericPollRepository;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.Polls.BaseWithVersion)]
        [RequireRoles("TeamLead,CEO,Employee")]
        public async Task<IActionResult> CreatePoll([FromBody] CreatePollRequest creationRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errors);
            }

            var poll = _mapper.Map<Poll>(creationRequest);
            poll.TeamId = (await _identityService.GetAppUserTeam(this.User)).Id;  
            poll.AppUserId = (await _identityService.GetAppUserAsync(this.User)).Id;

            if (await _genericPollRepository.CreateAsync(poll))
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpGet(ApiRoutes.Polls.BaseWithVersion)]
        public async Task<IActionResult> GetPolls()
        {
            var polls = await _genericPollRepository.GetAsync(includeFunc: polls => polls.Include(poll => poll.Team).Include(poll => poll.CreatedBy).Include(poll => poll.Options).ThenInclude(p => p.AppUserOptions), 
                                                              filter: poll => poll.TeamId == (_identityService.GetAppUserTeam(this.User)).Result.Id);

            var response = _mapper.Map<List<GetPollsResponse>>(polls);
            var appUserId = (await _identityService.GetAppUserAsync(this.User)).Id;

            foreach (var poll in response)
            {
                foreach (var option in poll.Options)
                {
                    option.Checked = polls.FirstOrDefault(polll => polll.Id == Guid.Parse(poll.Id)).Options.FirstOrDefault(opt => opt.Id == Guid.Parse(option.Id)).AppUserOptions.Any(auo => auo.AppUserId == _identityService.GetAppUserAsync(this.User).Result.Id);
                }
            }

            return Ok(response);
        }

        [HttpDelete(ApiRoutes.Polls.BaseWithVersion)]
        public async Task<IActionResult> DeletePoll([FromQuery] PollDeleteRequest request)
        {
            if (await _genericPollRepository.DeleteAsync(request.Id))
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpPut(ApiRoutes.Polls.BaseWithVersion)]
        public async Task<IActionResult> UpdatePoll([FromBody] PollUpdateRequest request)
        {
            var poll = await _genericPollRepository.GetByIdAsync(request.Id, includeFunc: polls => polls.Include(poll => poll.Options).ThenInclude(opt => opt.AppUserOptions).ThenInclude(opt => opt.AppUser));
            poll.Name = request.Name;
            poll.DoesAllowMultiple = request.DoesAllowMultiple;
            var options = _mapper.Map<List<Option>>(request.Options);

            for (int i = poll.Options.Count - 1; i >= 0; i--)
            {
                if (!options.Any(opt => opt.Id == poll.Options[i].Id))
                {
                    poll.Options.RemoveAt(i);
                }
            }

            foreach(var option in options)
            {
                if (option.Id == Guid.Empty)
                {
                    poll.Options.Add(option);
                }
            }

            CountOptions(poll);

            if (await _genericPollRepository.UpdateAsync(poll))
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpGet(ApiRoutes.Polls.GetById)]
        public async Task<IActionResult> GetById([FromQuery] Guid Id)
        {
            var poll = await _genericPollRepository.GetByIdAsync(Id, includeFunc: polls => polls.Include(poll => poll.Options).ThenInclude(opt=>opt.AppUserOptions));

            var pollResponse = _mapper.Map<GetPollsResponse>(poll);

            if (pollResponse != null)
            {
                return Ok(pollResponse);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost(ApiRoutes.Polls.MakeVote)]
        public async Task<IActionResult> MakeVote([FromBody] MakeVoteRequest request)
        {
            var poll = await _genericPollRepository.GetByIdAsync(request.PollId, polls => polls.Include(poll => poll.Options).ThenInclude(opt => opt.AppUserOptions));
            var appUserId = (await _identityService.GetAppUserAsync(this.User)).Id;
            var hasUserAlreadyVotedOnThatPoll = poll.Options.Any(opt => opt.AppUserOptions.Any(au => au.AppUserId == appUserId));

            foreach (var option in poll.Options)
            {
                var hasUserAlreadyVotedOnThisOption = option.AppUserOptions.Any(au => au.AppUserId == appUserId);
                if (option.Id == Guid.Parse(request.OptionId))
                {
                    if (!hasUserAlreadyVotedOnThisOption)
                    {
                        if (hasUserAlreadyVotedOnThatPoll && !poll.DoesAllowMultiple)
                        {
                            var optionSelected = poll.Options.FirstOrDefault(opt => opt.AppUserOptions.Any(au => au.AppUserId == appUserId));
                            var auo = optionSelected.AppUserOptions.FirstOrDefault(auo => auo.AppUserId == appUserId);
                            optionSelected.AppUserOptions.Remove(auo);
                        }

                        option.AppUserOptions.Add(new AppUserOption() { OptionId = Guid.Parse(request.OptionId), AppUserId = appUserId });
                    }
                    else
                    {
                        var auo = option.AppUserOptions.FirstOrDefault(auo => auo.AppUserId == appUserId);
                        option.AppUserOptions.Remove(auo);
                    }
                }
            }

            CountOptions(poll);

            if (await _genericPollRepository.UpdateAsync(poll))
            {
                var opts = _mapper.Map<List<GetPollsOptionsResponse>>(poll.Options);

                foreach (var opt in opts)
                {
                    opt.Checked = poll.Options.FirstOrDefault(optt => optt.Id == Guid.Parse(opt.Id)).AppUserOptions.Any(auo => auo.AppUserId == _identityService.GetAppUserAsync(this.User).Result.Id);
                }

                return Ok(opts);
            }

            return StatusCode(500);
        }

        private void CountOptions(Poll poll)
        {
            var totalNumberOfPeople = poll.Options.Where(opt => opt.AppUserOptions != null).SelectMany(p => p.AppUserOptions).Select(p => p.AppUser).Count();
            poll.CountOfPeopleVoted = totalNumberOfPeople;
            foreach(var option in poll.Options)
            {
                if (option.AppUserOptions != null)
                {
                    option.Value = option.AppUserOptions.Count != 0 ? ((float)option.AppUserOptions.Count / (float)poll.CountOfPeopleVoted) * 100 : 0;
                }
            }
        }
    }
}
