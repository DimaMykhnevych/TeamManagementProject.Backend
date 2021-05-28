using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.Authorization;
using TeamManagement.Contracts.v1;
using TeamManagement.Contracts.v1.Requests;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.Controllers
{
    [Authorize]
    public class TagController: ControllerBase
    {
        private readonly IGenericRepository<Tag> _genericRepository;
        private readonly IMapper _mapper;

        public TagController(IGenericRepository<Tag> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            this._mapper = mapper;
        }

        [RequireRoles("TeamLead,CEO,Employee")]
        [HttpGet(ApiRoutes.Tags.BaseWithVersion)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _genericRepository.GetAsync());
        }

        [RequireRoles("TeamLead,CEO,Employee")]
        [HttpPost(ApiRoutes.Tags.BaseWithVersion)]
        public async Task<IActionResult> Post([FromBody] TagCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage));
                return BadRequest(new { errors = errors });
            }

            var tag = _mapper.Map<Tag>(request);

            if (await _genericRepository.CreateAsync(tag))
            {
                return Ok(tag);
            }

            return StatusCode(500);
        }
    }
}
