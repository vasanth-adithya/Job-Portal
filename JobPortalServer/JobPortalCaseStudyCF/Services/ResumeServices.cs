using JobPortalCaseStudyCF.Controllers;
using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Models;
using JobPortalCaseStudyCF.Repositories;

namespace JobPortalCaseStudyCF.Services
{
    public class ResumeServices : IResumeServices
    {
        
        private readonly IRepository<Resume> _resumeRepository;
        private readonly ILogger<ResumeController> _logger;

        public ResumeServices(IRepository<Resume> resumeRepository, ILogger<ResumeController> logger)
        {
            _resumeRepository = resumeRepository;
            _logger = logger;
        }
        

        public async Task<Resume> CreateResumeAsync(Resume resume)
        {
            try
            {
                var createdResume = await _resumeRepository.CreateAsync(resume);
                return createdResume;
            }
            catch (Exception ex)
            {   
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteResumeAsync(int id)
        {
            try
            {
                var resume = await _resumeRepository.GetAsync(Resume => Resume.ResumeId == id, false);

                if (resume == null)
                {
                    _logger.LogError("Resume not found with given Id");
                    return false;
                }

                await _resumeRepository.DeleteAsync(resume);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<Resume> GetResumeByIdAsync(int id)
        {
            try
            {
                var resume = await _resumeRepository.GetAsync(Resume => Resume.ResumeId == id, false);

                if (resume == null)
                {
                    _logger.LogError("Resume not found with given Id");
                    return null;
                }
                return resume;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Resume>> GetResumeByJSIdAsync(int id)
        {
            try
            {
                var resumes = await _resumeRepository.GetAllAsync();
                var res = resumes.Where(a => a.JobSeekerId == id).ToList();

                if (res.Count == 0)
                {
                    _logger.LogError("Application not found with given Id");
                    return null;
                }
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<List<Resume>> GetAllResumesAsync()
        {
            try
            {
                var resumes = await _resumeRepository.GetAllAsync();
                return resumes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateResumeAsync(Resume resume)
        {
            try
            {
                var resumeUser = await _resumeRepository.GetAsync(item => item.ResumeId == resume.ResumeId, true);

                if (resumeUser == null)
                {
                    _logger.LogError("Resume not found with given Id");
                    return false;
                }

                await _resumeRepository.UpdateAsync(resume);
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
