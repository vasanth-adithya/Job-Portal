using JobPortalCaseStudyCF.Models;
using JobPortalCaseStudyCF.Models.DTO;

namespace JobPortalCaseStudyCF.Interfaces
{
    public interface IAccountServices
    {
        Task<Employer> RegisterEmployerAsync(RegisterEmployerDTO model);
        Task<JobSeeker> RegisterJobSeekerAsync(RegisterJobSeekerDTO model);

        string LoginAsync(dynamic model);
        public Task<dynamic> ForgotPassAsync(dynamic model);
        public Task<dynamic> ResetPassAsync(dynamic model);
    }
}
