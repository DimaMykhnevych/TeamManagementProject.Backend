using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.Authorization;
using TeamManagement.Contracts.v1;
using TeamManagement.Contracts.v1.Requests;
using TeamManagement.DataLayer.Data;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.Controllers
{
    [Authorize]
    public class TagController: ControllerBase
    {
        private readonly IGenericRepository<Tag> _genericRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TagController(IGenericRepository<Tag> genericRepository, 
            IMapper mapper, AppDbContext context, UserManager<AppUser> userManager)
        {
            _genericRepository = genericRepository;
            this._mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        [RequireRoles("TeamLead,CEO,Employee")]
        [HttpGet(ApiRoutes.Tags.BaseWithVersion)]
        public async Task<IActionResult> Get()
        {
            AppUser currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var tags = await _context.Tags.Where(t => t.TeamId == currentUser.TeamId).ToListAsync();
            return Ok(tags);
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
            AppUser currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            tag.TeamId = currentUser.TeamId;

            if (await _genericRepository.CreateAsync(tag))
            {
                return Ok(tag);
            }

            return StatusCode(500);
        }
    }
}
