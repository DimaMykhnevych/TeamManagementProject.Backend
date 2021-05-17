using System;
using System.Threading.Tasks;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.DataLayer.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company> Insert(Company entity);
        Task<Company> Get(Guid id);
        Task Save();
    }
}
