using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TeamManagement.DataLayer.Data;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.DataLayer.Repositories
{
    public class HowToArticlesRepository : BaseGenericRepository<HowToArticle>, IHowToArticlesRepository
    {
        public HowToArticlesRepository(AppDbContext context)
            : base(context)
        { }

        private readonly Func<IQueryable<HowToArticle>, IIncludableQueryable<HowToArticle, object>> _includeFunction =
            query => query.Include(howToArticle => howToArticle.Publisher);

        public Task<IEnumerable<HowToArticle>> GetAllAsync() =>
            GetAsync(includeFunc: _includeFunction);

        public Task<IEnumerable<HowToArticle>> SearchAsync(string searchWord)
        {
            searchWord = searchWord.Trim();
            Expression<Func<HowToArticle, bool>> filterFunction = howToArticle =>
                    howToArticle.Name.Contains(searchWord) ||
                    howToArticle.Problem.Contains(searchWord) ||
                    howToArticle.Solution.Contains(searchWord);

            return GetAsync(filter: filterFunction, includeFunc: _includeFunction);
        }
    }
}
