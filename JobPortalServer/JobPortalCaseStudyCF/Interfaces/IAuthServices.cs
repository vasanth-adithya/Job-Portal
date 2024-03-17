using JobPortalCaseStudyCF.Models;
using JobPortalCaseStudyCF.Models.DTO;

namespace JobPortalCaseStudyCF.Interfaces
{
    public interface IAuthServices
    {
        public string GenerateToken(dynamic user);
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedPassword);

    }
}
