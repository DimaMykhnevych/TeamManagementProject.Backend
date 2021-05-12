using System;
using System.Threading.Tasks;
using TeamManagement.DataLayer.Data;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.DataLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        protected readonly AppDbContext _context;
        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Company> Get(Guid id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<Company> Insert(Company company)
        {
            await _context.Companies.AddAsync(company);
            return company;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
