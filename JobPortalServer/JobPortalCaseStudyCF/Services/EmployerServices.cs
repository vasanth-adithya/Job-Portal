using JobPortalCaseStudyCF.Controllers;
using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Models;
using JobPortalCaseStudyCF.Repositories;

namespace JobPortalCaseStudyCF.Services
{
    public class EmployerServices : IEmployerServices
    {
        
        private readonly IRepository<Employer> _employerRepository;
        private readonly ILogger<EmployerController> _logger;
        private readonly IAuthServices _authServices;
        public EmployerServices(IRepository<Employer> employerRepository, ILogger<EmployerController> logger, IAuthServices authServices)
        {
            _employerRepository = employerRepository;
            _logger = logger;
            _authServices = authServices;

        }
        

        public async Task<Employer> CreateEmployerAsync(Employer employer)
        {
            try
            {
                var createdEmployer = await _employerRepository.CreateAsync(employer);
                return createdEmployer;
            }
            catch (Exception ex)
            {   
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteEmployerAsync(int id)
        {
            try
            {
                var employer = await _employerRepository.GetAsync(employer => employer.EmployerId == id, false);

                if (employer == null)
                {
                    _logger.LogError("Employer not found with given Id");
                    return false;
                }

                await _employerRepository.DeleteAsync(employer);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<Employer> GetEmployerByIdAsync(int id)
        {
            try
            {
                var employer = await _employerRepository.GetAsync(employer => employer.EmployerId == id, false);

                if (employer == null)
                {
                    _logger.LogError("Employer not found with given Id");
                    return null;
                }
                return employer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<Employer> GetEmployerByEmailasync(string email)
        {
            var emailLower = email.ToLower();
            var employer = await _employerRepository.GetAsync(employer => employer.Email.ToLower() == emailLower, false);

            if (employer == null)
            {
                _logger.LogError("Employer not found with given name");
                return null;
            }
            return employer;
        }

        public async Task<List<Employer>> GetAllEmployersAsync()
        {
            try
            {
                var employers = await _employerRepository.GetAllAsync();
                return employers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateEmployerAsync(Employer employer,bool flag)
        {
            try
            {
                var EmployerUser = await _employerRepository.GetAsync(item => item.EmployerId == employer.EmployerId, true);

                if (EmployerUser == null)
                {
                    _logger.LogError("Employer not found with given Id");
                    return false;
                }
                if (flag)
                {
                    var hashedPassword = _authServices.HashPassword(employer.Password);
                    employer.Password = hashedPassword;
                }


                await _employerRepository.UpdateAsync(employer);
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
