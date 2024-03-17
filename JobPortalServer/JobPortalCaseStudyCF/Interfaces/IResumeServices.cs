using JobPortalCaseStudyCF.Models;

namespace JobPortalCaseStudyCF.Interfaces
{
    public interface IResumeServices
    {
        public Task<List<Resume>> GetAllResumesAsync();
        public Task<Resume> GetResumeByIdAsync(int id);
        public Task<List<Resume>> GetResumeByJSIdAsync(int id);
        public Task<Resume> CreateResumeAsync(Resume resume);
        public Task<bool> UpdateResumeAsync(Resume resume);
        public Task<bool> DeleteResumeAsync(int id);
    }
}
