using JobPortalCaseStudyCF.Models;

namespace JobPortalCaseStudyCF.Interfaces
{
    public interface IApplicationServices
    {
        public Task<List<Application>> GetAllApplicationsAsync();
        public Task<Application> GetApplicationByIdAsync(int id);
        public Task<List<Application>> GetApplicationByJSIdAsync(int id);
        public Task<List<Application>> GetApplicationByListingIdAsync(int id);
        public Task<IEnumerable<Application>> GetApplicationByEmployerIDAsync(int id);
        public Task<Application> CreateApplicationAsync(Application application);
        public Task<bool> UpdateApplicationAsync(Application application);
        public Task<bool> DeleteApplicationAsync(int id);
    }
}
