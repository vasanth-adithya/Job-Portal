using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Mappers;
using JobPortalCaseStudyCF.Models;
using JobPortalCaseStudyCF.Models.DTO;
using JobPortalCaseStudyCF.Repositories;
using System.Security.Cryptography;

namespace JobPortalCaseStudyCF.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly ILogger<AccountServices> _logger;
        private readonly IAuthServices _authServices;
        private readonly IEmployerServices _employerServices;
        private readonly IJobSeekerServices _jobseekerServices;

        public AccountServices(ILogger<AccountServices> logger, IAuthServices authServices, IEmployerServices employerServices, IJobSeekerServices jobseekerServices)
        {
            _logger = logger;
            _authServices = authServices;
            _employerServices = employerServices;
            _jobseekerServices = jobseekerServices;
        }
        public string LoginAsync(dynamic user)
        {
            var jwt = _authServices.GenerateToken(user);
            
            return jwt;
        }

        public async Task<Employer> RegisterEmployerAsync(RegisterEmployerDTO registrationData)
        {
            string hashedPassword = _authServices.HashPassword(registrationData.Password);
            registrationData.Password = hashedPassword;

            RegistertoEmployer registerToUser = new RegistertoEmployer(registrationData);
            Employer userToRegister = registerToUser.GetUser();

            var createdUser = await _employerServices.CreateEmployerAsync(userToRegister);
            createdUser.Password = null;

            return createdUser;
        }

        public async Task<JobSeeker> RegisterJobSeekerAsync(RegisterJobSeekerDTO registrationData)
        {
            string hashedPassword = _authServices.HashPassword(registrationData.Password);
            registrationData.Password = hashedPassword;

            RegistertoJobSeeker registerToUser = new RegistertoJobSeeker(registrationData);
            JobSeeker userToRegister = registerToUser.GetUser();

            var createdUser = await _jobseekerServices.CreateJobSeekerAsync(userToRegister);
            createdUser.Password = null;

            return createdUser;
        }
        public async Task<dynamic> ForgotPassAsync(dynamic model)
        {
            model.Token = await generateResetToken(model.Role);
            model.ResetPasswordExpires = DateTime.UtcNow.AddHours(6);

            if (model.Role == "Employer")
            {
                await _employerServices.UpdateEmployerAsync(model, true);
            }
            else
            {
                await _jobseekerServices.UpdateJobSeekerAsync(model,true);
            }
            return model;
        }

        public async Task<dynamic> ResetPassAsync(dynamic model)
        {
            dynamic user = null;
            var employers=await _employerServices.GetAllEmployersAsync();
            user = employers.Where(u => u.Token == model.Token).FirstOrDefault();
            if (user == null)
            {
                var jobSeekers= await _jobseekerServices.GetAllJobSeekersAsync();
                user = jobSeekers.Where(u => u.Token == model.Token).FirstOrDefault();
            }

            if (user == null)
            {
                return null;
            }

            if (user.ResetPasswordExpires < DateTime.UtcNow)
            {
                return null;
            }

            string hashedPassword = _authServices.HashPassword(model.Password);
            user.Password = hashedPassword;

            if (user.Role == "Employer")
            {
                user = await _employerServices.UpdateEmployerAsync(user,false);

            }
            else
            {
                user = await _jobseekerServices.UpdateJobSeekerAsync(user,false);
            }

            return user;

        }


        private async Task<string> generateResetToken(string role)
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            bool tokenIsUnique = false;
            if (role == "Employer")
            {
                var users = await _employerServices.GetAllEmployersAsync();
                tokenIsUnique = !users.Any(u => u.Token == token);
            }
            else if (role == "JobSeeker")
            {
                var users = await _jobseekerServices.GetAllJobSeekersAsync();
                tokenIsUnique = !users.Any(u => u.Token == token);
            }
            if (!tokenIsUnique)
                return await generateResetToken(role);

            return token;
        }

    }

}

