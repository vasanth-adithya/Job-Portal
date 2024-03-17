using JobPortalCaseStudyCF.Models.DTO;
using JobPortalCaseStudyCF.Models;

namespace JobPortalCaseStudyCF.Mappers
{
    public class RegistertoEmployer
    {
        Employer employer;

        public RegistertoEmployer(RegisterEmployerDTO registerUser)
        {
            employer = new Employer();
            employer.EmployerName = registerUser.EmployerName;
            employer.UserName = registerUser.UserName;
            employer.Email = registerUser.Email;
            employer.Gender = registerUser.Gender;
            employer.Role = "Employer";
            employer.Password = registerUser.Password;
            employer.CompanyName = registerUser.CompanyName;
            employer.ContactPhone = registerUser.ContactPhone;
            employer.CwebsiteUrl = registerUser.CwebsiteUrl;
        }
        public Employer GetUser() 
        {
            return employer;
        }
    }
}
