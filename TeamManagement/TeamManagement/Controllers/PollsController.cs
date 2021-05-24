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
        [RequireRoles("Administrator")]
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
            var poll = await _genericPollRepository.GetByIdAsync(request.Id); 

            if (await _genericPollRepository.UpdateAsync(poll))
            {
                return Ok();
            }

            return StatusCode(500);
        }

        [HttpPost(ApiRoutes.Polls.MakeVote)]
        public async Task<IActionResult> MakeVote([FromBody] MakeVoteRequest request)
        {
            var poll = await _genericPollRepository.GetByIdAsync(request.PollId, polls => polls.Include(poll => poll.Options).ThenInclude(opt => opt.AppUserOptions));
            var appUserId = (await _identityService.GetAppUserAsync(this.User)).Id;
            var wasVoted = false;
            var wasUserNotDeleted = true;

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
                        wasVoted = true;
                    }
                    else
                    {
                        var auo = option.AppUserOptions.FirstOrDefault(auo => auo.AppUserId == appUserId);
                        option.AppUserOptions.Remove(auo);
                        poll.CountOfPeopleVoted--;
                        wasUserNotDeleted = false;
                    }
                }
            }

            if (!hasUserAlreadyVotedOnThatPoll || (poll.DoesAllowMultiple && wasVoted) && wasUserNotDeleted)
            {
                poll.CountOfPeopleVoted++;
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
            foreach(var option in poll.Options)
            {
                option.Value = option.AppUserOptions.Count != 0 ? ((float)option.AppUserOptions.Count / (float)poll.CountOfPeopleVoted) * 100 : 0;
            }
        }
    }
}
