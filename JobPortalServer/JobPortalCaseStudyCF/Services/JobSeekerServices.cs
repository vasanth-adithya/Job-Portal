using JobPortalCaseStudyCF.Controllers;
using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Models;

namespace JobPortalCaseStudyCF.Services
{
    public class JobSeekerServices : IJobSeekerServices
    {
        private readonly IRepository<JobSeeker> _jobseekerRepository;
        private readonly ILogger<JobSeekerController> _logger;
        private readonly IAuthServices _authServices;
        public JobSeekerServices(IRepository<JobSeeker> jobseekerRepository, ILogger<JobSeekerController> logger, IAuthServices authServices)
        {
            _jobseekerRepository = jobseekerRepository;
            _logger = logger;
            _authServices = authServices;
        }

        public async Task<List<JobSeeker>> GetAllJobSeekersAsync()
        {
            try
            {
                var jobseekers = await _jobseekerRepository.GetAllAsync();
                return jobseekers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<JobSeeker> GetJobSeekerByIdAsync(int id)
        {
            try
            {
                var jobseeker = await _jobseekerRepository.GetAsync(jobseeker => jobseeker.JobSeekerId == id, false);

                if (jobseeker == null)
                {
                    _logger.LogError("JobSeeker not found with given Id");
                    return null;
                }
                return jobseeker;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<JobSeeker> GetJobSeekerByEmailAsync(string email)
        {
            var emailLower = email.ToLower();
            var jobseeker = await _jobseekerRepository.GetAsync(jobseeker => jobseeker.Email.ToLower() == emailLower, false);

            if (jobseeker == null)
            {
                _logger.LogError("JobSeeker not found with given name");
                return null;
            }
            return jobseeker;
        }

        public async Task<JobSeeker> CreateJobSeekerAsync(JobSeeker jobseekers)
        {
            try
            {
                var createdJobSeeker = await _jobseekerRepository.CreateAsync(jobseekers);
                return createdJobSeeker;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }            
        }               

        public async Task<bool> UpdateJobSeekerAsync(JobSeeker jobseekers, bool flag)
        {
            try
            {
                var jobseeker = await _jobseekerRepository.GetAsync(item => item.JobSeekerId == jobseekers.JobSeekerId, true);

                if (jobseeker == null)
                {
                    _logger.LogError("JobSeeker not found with given Id");
                    return false;
                }
                if (flag)
                {
                    var hashedPassword = _authServices.HashPassword(jobseekers.Password);
                    jobseekers.Password = hashedPassword;
                }
                await _jobseekerRepository.UpdateAsync(jobseekers);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }            
        }

        public async Task<bool> DeleteJobSeekerAsync(int id)
        {
            try
            {
                var jobseeker = await _jobseekerRepository.GetAsync(jobseeker => jobseeker.JobSeekerId == id, false);

                if (jobseeker == null)
                {
                    _logger.LogError("JobSeeker not found with given Id");
                    return false;
                }

                await _jobseekerRepository.DeleteAsync(jobseeker);
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
