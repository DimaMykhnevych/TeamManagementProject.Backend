using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DataLayer.Domain.Models;

namespace TeamManagement.DataLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetUsers();
    }
}
