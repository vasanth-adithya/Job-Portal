using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortalCaseStudyCF.Models
{
    public class Employer
    {
        public Employer()
        {
            JobListings = new List<JobListing>();
        }
        public int EmployerId { get; set; }
        public string EmployerName { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string CompanyName { get; set; }
        public string ContactPhone { get; set; } 
        public string CwebsiteUrl { get; set; }
        public string? Role { get; set; }
        [NotMapped]
        public string? Token { get; set; }
        public DateTime? ResetPasswordExpires { get; set; }
        public virtual List<JobListing> JobListings { get; set; }

    }
}
