using System.ComponentModel.DataAnnotations;
namespace JobPortalCaseStudyCF.Models.DTO
{
    public class RegisterEmployerDTO
    {
        [Required]
        public string EmployerName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string ContactPhone { get; set; }
        [Required]
        public string CwebsiteUrl { get; set; }
    }
}
