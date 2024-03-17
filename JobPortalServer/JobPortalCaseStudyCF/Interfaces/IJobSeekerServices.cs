using JobPortalCaseStudyCF.Models;

namespace JobPortalCaseStudyCF.Interfaces
{
    public interface IJobSeekerServices
    {
        public Task<List<JobSeeker>> GetAllJobSeekersAsync(); 
        public Task<JobSeeker> GetJobSeekerByIdAsync(int id);
        public Task<JobSeeker> GetJobSeekerByEmailAsync(string email);
        public Task<JobSeeker> CreateJobSeekerAsync(JobSeeker jobseekers);
        public Task<bool> UpdateJobSeekerAsync(JobSeeker jobseekers, bool flag);
        public Task<bool> DeleteJobSeekerAsync(int id);

    }
}
