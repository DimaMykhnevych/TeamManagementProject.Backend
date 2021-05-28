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

namespace TeamManagement.Controllers
{
    [ApiController]
    [Authorize]
    public class ArticlesController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IGenericRepository<Article> _genericArticleRepository;
        private readonly IMapper _mapper;

        public ArticlesController(IIdentityService identityService, IGenericRepository<Article> genericArticleRepository, IMapper mapper)
        {
            _identityService = identityService;
            _genericArticleRepository = genericArticleRepository;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.Articles.BaseWithVersion)]
        [RequireRoles("TeamLead,CEO,Employee")]
        public async Task<IActionResult> CreateArticle([FromBody] ArticleCreateRequest creationRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errors);
            }

            var article = _mapper.Map<Article>(creationRequest);

            if (await _genericArticleRepository.CreateAsync(article))
            {
                var response = _mapper.Map<ArticleCreateResponse>(article);
                return Created(Url.Action("GetArticleById", new { id = article.Id }), response);
            }

            return StatusCode(500);
        }

        [HttpGet(ApiRoutes.Articles.GetById)]
        public async Task<IActionResult> GetArticleById(Guid id)
        {
            var article = await _genericArticleRepository.GetByIdAsync(id, article => article.Include(article => article.Publisher).Include(article => article.Tag));

            if (article == null)
            {
                return NotFound(new { message = "Article was not found" });
            }

            var response = _mapper.Map<ArticleGetByIdResponse>(article);
            return Ok(response);
        }

        [HttpPut(ApiRoutes.Articles.Update)]
        public async Task<IActionResult> UpdateArticle(Guid id, [FromBody] ArticleUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errors);
            }

            if (id == Guid.Empty)
            {
                return BadRequest(new { errors = new string[] { "Id was null" } });
            }

            var dbArticle = await _genericArticleRepository.GetByIdAsync(id, x => x.Include(article => article.Publisher));

            if (dbArticle == null)
            {
                return NotFound(new { errors = new string[] { "Article was not found" } });
            }

            _mapper.Map(request, dbArticle);

            if (dbArticle.Publisher != await _identityService.GetAppUserAsync(this.User))
            {
                return Forbid();
            }

            if (await _genericArticleRepository.UpdateAsync(dbArticle))
            {
                return Ok(new { message = "Article updated successfully" });
            }

            return StatusCode(500);
        }

        [HttpDelete(ApiRoutes.Articles.Delete)]
        public async Task<IActionResult> DeleteArticle(Guid id)
        {
            var dbArticle = await _genericArticleRepository.GetByIdAsync(id, x => x.Include(article => article.Publisher));

            if (dbArticle == null)
            {
                return NotFound(new { message = "Article was not found" });
            }

            if (dbArticle.Publisher != await _identityService.GetAppUserAsync(this.User))
            {
                return Forbid();
            }

            if (await _genericArticleRepository.DeleteAsync(dbArticle))
            {
                return Ok(new { message = "Article deleted successfully" });
            }

            return StatusCode(500);
        }

        [HttpGet(ApiRoutes.Articles.BaseWithVersion)]
        public async Task<IActionResult> Get(bool grouped)
        {
            if (grouped)
            {
                var responseArticles = await _genericArticleRepository.GetWithGroupByAsync(
                    article => article.Tag.Label,
                    group => new ArticleGetResponse
                    {
                        Tag = group.Key.ToString(),
                        Articles = group.Select(article => _mapper.Map<ArticleMenuResponse>(article)).ToList()
                    },
                    article => article.Include(article => article.Tag),
                    article => article.Status == "Published");

                return Ok(responseArticles);
            }
            else
            {
                var responseArticles = await _genericArticleRepository.GetWithSelectAsync(article => _mapper.Map<ArticleMenuResponse>(article));
                return Ok(responseArticles);
            }
        }

        [HttpGet(ApiRoutes.Articles.GetForCurrentUser)]
        public async Task<IActionResult> GetArticlesForCurrentUser()
        {
            var articles = await _genericArticleRepository.GetWithSelectAsync(article => _mapper.Map<ArticleMenuResponse>(article),
                                                                              article => article.Publisher.UserName == User.Identity.Name);
            return Ok(articles);
        }
    }
}
