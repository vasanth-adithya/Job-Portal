using JobPortalCaseStudyCF.Context;
using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace JobPortalCaseStudyCF.Repositories
{
    public class ApplicationRepository : IRepository<Application>
    {
        private readonly JobPortalCFContext _context;
        public ApplicationRepository(JobPortalCFContext context)
        {
            _context = context;
        }

        public async Task<Application> CreateAsync(Application dbRecord)
        {
            _context.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Application dbRecord)
        {
            _context.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Application>> GetAllAsync()
        {
            return await _context.Applications.ToListAsync();
        }

        public async Task<Application> GetAsync(Expression<Func<Application, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Applications.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _context.Applications.Where(filter).FirstOrDefaultAsync();
        }

        //public async Task<List<Application>> GetAllByJSAsync(int id)
        //{
        //    return await _context.Applications.Where(a=>a.JobSeekerId==id).ToListAsync();
        //}

        public async Task<Application> UpdateAsync(Application dbRecord)
        {
            _context.Applications.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
