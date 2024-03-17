using JobPortalCaseStudyCF.Models.DTO;
using JobPortalCaseStudyCF.Models;

namespace JobPortalCaseStudyCF.Mappers
{
    public class RegistertoJobSeeker
    {
        JobSeeker jobseeker;
        public RegistertoJobSeeker(RegisterJobSeekerDTO registerUser)
        {
            jobseeker = new JobSeeker();
            jobseeker.JobSeekerName = registerUser.JobSeekerName;
            jobseeker.UserName = registerUser.UserName;
            jobseeker.Email = registerUser.Email;
            jobseeker.Role = "JobSeeker";
            jobseeker.Password = registerUser.Password;
            jobseeker.Gender = registerUser.Gender;
            jobseeker.ContactPhone = registerUser.ContactPhone;
            jobseeker.Address = registerUser.Address;
            jobseeker.Description = registerUser.Description;
            jobseeker.DateOfBirth = registerUser.DateOfBirth;
            jobseeker.Qualification = registerUser.Qualification;
            jobseeker.Specialization = registerUser.Specialization;
            jobseeker.Institute = registerUser.Institute;
            jobseeker.Year = registerUser.Year;
            jobseeker.CGPA = registerUser.CGPA;
            jobseeker.CompanyName = registerUser.CompanyName;
            jobseeker.Position = registerUser.Position;
            jobseeker.Responsibilities = registerUser.Responsibilities;
            jobseeker.StartDate = registerUser.StartDate;
            jobseeker.EndDate = registerUser.EndDate;

        }
        public JobSeeker GetUser()
        {
            return jobseeker;
        }
    }
}
