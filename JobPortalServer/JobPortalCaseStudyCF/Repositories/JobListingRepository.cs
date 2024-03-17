using JobPortalCaseStudyCF.Context;
using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace JobPortalCaseStudyCF.Repositories
{
    public class JobListingRepository : IRepository<JobListing>
    {
        private readonly JobPortalCFContext _context;
        public JobListingRepository(JobPortalCFContext context)
        {
            _context = context;
        }

        public async Task<JobListing> CreateAsync(JobListing dbRecord)
        {
            _context.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(JobListing dbRecord)
        {
            _context.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<JobListing>> GetAllAsync()
        {
            return await _context.JobListings.ToListAsync();
        }

        public async Task<JobListing> GetAsync(Expression<Func<JobListing, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.JobListings.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _context.JobListings.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<JobListing> UpdateAsync(JobListing dbRecord)
        {
            _context.JobListings.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
