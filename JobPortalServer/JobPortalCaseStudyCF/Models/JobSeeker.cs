using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortalCaseStudyCF.Models
{
    public  class JobSeeker
    {
        public JobSeeker()
        {
            Applications = new List<Application>();
            Resumes = new List<Resume>();
        }
        public int JobSeekerId { get; set; }
        public string JobSeekerName { get; set; }
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Qualification { get; set; }
        public string Specialization { get; set; }
        public string Institute { get; set; }
        public int Year { get; set; }
        public decimal CGPA { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string Responsibilities { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Role { get; set; }
        [NotMapped]
        public string? Token { get; set; }
        public DateTime? ResetPasswordExpires { get; set; }
        public virtual List<Application> Applications { get; set; }
        public virtual List<Resume> Resumes { get; set; }
    }
}
