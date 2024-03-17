using JobPortalCaseStudyCF.Controllers;
using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Models;
using JobPortalCaseStudyCF.Repositories;

namespace JobPortalCaseStudyCF.Services
{
    public class JobListingServices : IJobListingServices
    {
        
        private readonly IRepository<JobListing> _jobListingRepository;
        private readonly ILogger<JobListingController> _logger;

        public JobListingServices(IRepository<JobListing> jobListingRepository, ILogger<JobListingController> logger)
        {
            _jobListingRepository = jobListingRepository;
            _logger = logger;
        }
        

        public async Task<JobListing> CreateJobListingAsync(JobListing jobListing)
        {
            try
            {
                var createdJobListing = await _jobListingRepository.CreateAsync(jobListing);
                return createdJobListing;
            }
            catch (Exception ex)
            {   
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteJobListingAsync(int id)
        {
            try
            {
                var jobListing = await _jobListingRepository.GetAsync(JobListing => JobListing.ListingId == id, false);

                if (jobListing == null)
                {
                    _logger.LogError("JobListing not found with given Id");
                    return false;
                }

                await _jobListingRepository.DeleteAsync(jobListing);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<JobListing> GetJobListingByIdAsync(int id)
        {
            try
            {
                var jobListing = await _jobListingRepository.GetAsync(JobListing => JobListing.ListingId == id, false);

                if (jobListing == null)
                {
                    _logger.LogError("JobListing not found with given Id");
                    return null;
                }
                return jobListing;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<List<JobListing>> GetJobListingByEmployerIdAsync(int id)
        {
            try
            {
                var jobListings = await _jobListingRepository.GetAllAsync();
                var job = jobListings.Where(a => a.EmployerId == id).ToList();

                if (job.Count == 0)
                {
                    _logger.LogError("Listing not found with given Id");
                    return null;
                }
                return job;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<List<JobListing>> GetAllJobListingsAsync()
        {
            try
            {
                var jobListings = await _jobListingRepository.GetAllAsync();
                return jobListings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateJobListingAsync(JobListing jobListing)
        {
            try
            {
                var jobListingUser = await _jobListingRepository.GetAsync(item => item.ListingId == jobListing.ListingId, true);

                if (jobListingUser == null)
                {
                    _logger.LogError("JobListing not found with given Id");
                    return false;
                }

                await _jobListingRepository.UpdateAsync(jobListing);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
