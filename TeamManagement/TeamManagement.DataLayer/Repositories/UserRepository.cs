using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DataLayer.Data;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _context.AppUsers.ToListAsync();
        }

        public async Task<AppUser> GetUserWithCompany(string userName)
        {
            return await _context.AppUsers.Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<List<AppUser>> GetUsersWithCompanies()
        {
            return await _context.AppUsers.Include(u => u.Company).ToListAsync();
        }
    }
}
