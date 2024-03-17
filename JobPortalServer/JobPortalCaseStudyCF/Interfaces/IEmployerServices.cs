using JobPortalCaseStudyCF.Models;

namespace JobPortalCaseStudyCF.Interfaces
{
    public interface IEmployerServices
    {
        public Task<List<Employer>> GetAllEmployersAsync();
        public Task<Employer> GetEmployerByIdAsync(int id);
        public Task<Employer> GetEmployerByEmailasync(string email);
        public Task<Employer> CreateEmployerAsync(Employer employer);
        public Task<bool> UpdateEmployerAsync(Employer employer,bool flag);
        public Task<bool> DeleteEmployerAsync(int id);
    }
}
