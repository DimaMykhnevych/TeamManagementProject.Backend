using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.DataLayer.Repositories.Interfaces
{
    public interface IHowToArticlesRepository : IGenericRepository<HowToArticle>
    {
        Task<IEnumerable<HowToArticle>> GetAllAsync();
        Task<IEnumerable<HowToArticle>> SearchAsync(string searchWord);
    }
}
