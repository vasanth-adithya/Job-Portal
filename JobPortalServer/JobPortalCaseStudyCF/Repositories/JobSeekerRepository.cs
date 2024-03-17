using JobPortalCaseStudyCF.Context;
using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace JobPortalCaseStudyCF.Repositories
{
    public class JobSeekerRepository : IRepository<JobSeeker>
    {
        private readonly JobPortalCFContext _context;
        public JobSeekerRepository(JobPortalCFContext context)
        {
            _context = context;
        }

        public async Task<JobSeeker> CreateAsync(JobSeeker dbRecord)
        {
            _context.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(JobSeeker dbRecord)
        {
            _context.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<JobSeeker>> GetAllAsync()
        {
            return await _context.JobSeekers.ToListAsync();
        }

        public async Task<JobSeeker> GetAsync(Expression<Func<JobSeeker, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.JobSeekers.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _context.JobSeekers.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<JobSeeker> UpdateAsync(JobSeeker dbRecord)
        {
            _context.JobSeekers.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
