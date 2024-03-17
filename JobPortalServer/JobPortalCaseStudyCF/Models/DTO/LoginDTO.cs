﻿using System.ComponentModel.DataAnnotations;
namespace JobPortalCaseStudyCF.Models.DTO
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
