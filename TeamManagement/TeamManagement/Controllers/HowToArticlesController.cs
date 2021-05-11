using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1;
using TeamManagement.Contracts.v1.Requests;
using TeamManagement.Contracts.v1.Responses;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.Controllers
{
    [Authorize]
    [ApiController]
    public class HowToArticlesController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IHowToArticlesRepository _howToArticleRepository;
        private readonly IMapper _mapper;

        public HowToArticlesController(IIdentityService identityService,
            IHowToArticlesRepository repository, IMapper mapper)
        {
            _identityService = identityService;
            _howToArticleRepository = repository;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.HowToArticles.BaseWithVersion)]
        public async Task<IActionResult> CreateHowToArticle([FromBody] HowToArticleCreateRequest creationRequest)
        {
            var howToArticle = _mapper.Map<HowToArticle>(creationRequest);

            if (await _howToArticleRepository.CreateAsync(howToArticle))
            {
                var response = _mapper.Map<HowToArticleCreateResponse>(howToArticle);
                return Created(Url.Action("GetHowToArticleById", new { id = howToArticle.Id }), response);
            }

            return StatusCode(500);
        }

        [HttpGet(ApiRoutes.HowToArticles.BaseWithVersion)]
        public async Task<IActionResult> GetHowToArticles([FromQuery] string search)
        {
            IEnumerable<HowToArticle> howToArticles = (search != null) ?
                await _howToArticleRepository.SearchAsync(search) :
                await _howToArticleRepository.GetAllAsync();

            var howToArticleDTOs = _mapper.Map<HowToArticleGetResponse[]>(howToArticles);
            return Ok(howToArticleDTOs);
        }

        [HttpGet(ApiRoutes.HowToArticles.GetById)]
        public async Task<IActionResult> GetHowToArticleById(Guid id)
        {
            var howToArticle = await _howToArticleRepository.GetByIdAsync(id, query =>
                query.Include(howToArticle => howToArticle.Publisher));

            if (howToArticle == null)
            {
                return NotFound(new { message = "How-to article was not found" });
            }

            var response = _mapper.Map<HowToArticleGetResponse>(howToArticle);
            return Ok(response);
        }

        [HttpDelete(ApiRoutes.HowToArticles.Delete)]
        public async Task<IActionResult> DeleteHowToArticle(Guid id)
        {
            var howToArticle = await _howToArticleRepository.GetByIdAsync(id, query =>
                query.Include(article => article.Publisher));

            if (howToArticle == null)
            {
                return NotFound(new { message = "How-to article was not found" });
            }

            if (howToArticle.Publisher != await _identityService.GetAppUserAsync(User))
            {
                return Forbid();
            }

            if (await _howToArticleRepository.DeleteAsync(howToArticle))
            {
                return Ok(new { message = "How-to article was deleted successfully" });
            }

            return StatusCode(500);
        }

        [HttpPut(ApiRoutes.HowToArticles.Update)]
        public async Task<IActionResult> UpdateHowToArticle(Guid id, [FromBody] HowToArticleUpdateRequest howToArticleDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errors);
            }

            var howToArticle = await _howToArticleRepository.GetByIdAsync(id, query =>
                query.Include(article => article.Publisher));

            if (howToArticle == null)
            {
                return NotFound(new { message = "How-to article was not found" });
            }

            if (howToArticle.Publisher != await _identityService.GetAppUserAsync(User))
            {
                return Forbid();
            }

            _mapper.Map(howToArticleDTO, howToArticle);
            if (await _howToArticleRepository.UpdateAsync(howToArticle))
            {
                return Ok(new { message = "How-to article updated successfully" });
            }

            return StatusCode(500);
        }
    }
}
