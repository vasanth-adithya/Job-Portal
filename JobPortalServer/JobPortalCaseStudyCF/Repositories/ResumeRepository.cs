using JobPortalCaseStudyCF.Context;
using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace JobPortalCaseStudyCF.Repositories
{
    public class ResumeRepository : IRepository<Resume>
    {
        private readonly JobPortalCFContext _context;
        public ResumeRepository(JobPortalCFContext context)
        {
            _context = context;
        }

        public async Task<Resume> CreateAsync(Resume dbRecord)
        {
            _context.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Resume dbRecord)
        {
            _context.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Resume>> GetAllAsync()
        {
            return await _context.Resumes.ToListAsync();
        }

        public async Task<Resume> GetAsync(Expression<Func<Resume, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Resumes.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _context.Resumes.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<Resume> UpdateAsync(Resume dbRecord)
        {
            _context.Resumes.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
