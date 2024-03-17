using JobPortalCaseStudyCF.Models;

namespace JobPortalCaseStudyCF.Interfaces
{
    public interface IJobListingServices
    {
        public Task<List<JobListing>> GetAllJobListingsAsync();
        public Task<JobListing> GetJobListingByIdAsync(int id);
        public Task<List<JobListing>> GetJobListingByEmployerIdAsync(int id);
        public Task<JobListing> CreateJobListingAsync(JobListing jobListing);
        public Task<bool> UpdateJobListingAsync(JobListing jobListing);
        public Task<bool> DeleteJobListingAsync(int id);
    }
}
