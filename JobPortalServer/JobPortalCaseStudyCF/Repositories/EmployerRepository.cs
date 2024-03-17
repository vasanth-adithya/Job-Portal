using JobPortalCaseStudyCF.Context;
using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace JobPortalCaseStudyCF.Repositories
{
    public class EmployerRepository : IRepository<Employer>
    {
        private readonly JobPortalCFContext _context;
        public EmployerRepository(JobPortalCFContext context)
        {
            _context = context;
        }

        public async Task<Employer> CreateAsync(Employer dbRecord)
        {
            _context.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Employer dbRecord)
        {
            _context.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Employer>> GetAllAsync()
        {
            return await _context.Employers.ToListAsync();
        }

        public async Task<Employer> GetAsync(Expression<Func<Employer, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Employers.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _context.Employers.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<Employer> UpdateAsync(Employer dbRecord)
        {
            _context.Employers.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
