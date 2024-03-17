using JobPortalCaseStudyCF.Controllers;
using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Models;
using JobPortalCaseStudyCF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalCaseStudyCF.Services
{
    public class ApplicationServices : IApplicationServices
    {
        
        private readonly IRepository<Application> _applicationRepository;
        private readonly ILogger<ApplicationController> _logger;
        private readonly IRepository<JobListing> _listingRepository;

        public ApplicationServices(IRepository<Application> applicationRepository, IRepository<JobListing> listingRepository ,ILogger<ApplicationController> logger)
        {
            _applicationRepository = applicationRepository;
            _logger = logger;
            _listingRepository = listingRepository;
        }
        

        public async Task<Application> CreateApplicationAsync(Application application)
        {
            try
            {
                var createdApplication = await _applicationRepository.CreateAsync(application);
                return createdApplication;
            }
            catch (Exception ex)
            {   
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteApplicationAsync(int id)
        {
            try
            {
                var application = await _applicationRepository.GetAsync(Application => Application.ApplicationId == id, false);

                if (application == null)
                {
                    _logger.LogError("Application not found with given Id");
                    return false;
                }

                await _applicationRepository.DeleteAsync(application);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<Application> GetApplicationByIdAsync(int id)
        {
            try
            {
                var application = await _applicationRepository.GetAsync(Application => Application.ApplicationId == id, false);

                if (application == null)
                {
                    _logger.LogError("Application not found with given Id");
                    return null;
                }
                return application;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Application>> GetApplicationByJSIdAsync(int id)
        {
            try
            {
                var application = await _applicationRepository.GetAllAsync();
                var app = application.Where(a => a.JobSeekerId == id).ToList();
 
                if (app.Count==0)
                {
                    _logger.LogError("Application not found with given Id");
                    return null;
                }
                return app;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Application>> GetApplicationByListingIdAsync(int id)
        {
            try
            {
                var application = await _applicationRepository.GetAllAsync();
                var app = application.Where(a => a.ListingId == id).ToList();

                if (app.Count == 0)
                {
                    _logger.LogError("Application not found with given Id");
                    return null;
                }
                return app;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Application>> GetApplicationByEmployerIDAsync(int id)
        {
            try
            {
                var application = await _applicationRepository.GetAllAsync();
                var listings = await _listingRepository.GetAllAsync();
                var joblistingbyeid = from Application app in application
                              join JobListing listing in listings on app.ListingId equals listing.ListingId
                              where listing.EmployerId == id
                              select app;
                if (joblistingbyeid.Count() == 0)
                {
                    _logger.LogError("Application not found with given Id");
                    return null;
                }
                return joblistingbyeid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<List<Application>> GetAllApplicationsAsync()
        {
            try
            {
                var applications = await _applicationRepository.GetAllAsync();
                return applications;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateApplicationAsync(Application application)
        {
            try
            {
                var applicationUser = await _applicationRepository.GetAsync(item => item.ApplicationId == application.ApplicationId, true);

                if (applicationUser == null)
                {
                    _logger.LogError("Application not found with given Id");
                    return false;
                }

                await _applicationRepository.UpdateAsync(application);
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
