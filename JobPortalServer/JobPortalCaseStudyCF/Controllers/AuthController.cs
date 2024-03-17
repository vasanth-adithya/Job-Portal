using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Mappers;
using JobPortalCaseStudyCF.Models;
using JobPortalCaseStudyCF.Models.DTO;
using JobPortalCaseStudyCF.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace JobPortalCaseStudyCF.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthServices _authServices;
        private readonly IEmployerServices _employerServices;
        private readonly IJobSeekerServices _jobseekerServices;
        private readonly IAccountServices _accountServices;
        private readonly IEmailService _emailService;

        public AuthController(ILogger<AuthController> logger, IAuthServices authServices, IEmployerServices employerServices, IJobSeekerServices jobseekerServices, IAccountServices accountServices, IEmailService emailService)
        {
            _logger = logger;
            _authServices = authServices;
            _accountServices = accountServices;
            _employerServices = employerServices;
            _jobseekerServices = jobseekerServices;
            _emailService = emailService;
        }


        //Employer Registration
        [HttpPost]
        [Route("Employer/Register")]
        public async Task<ActionResult> RegisterEmployer([FromBody] RegisterEmployerDTO registrationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (registrationData.Password != registrationData.ConfirmPassword)
            {
                ModelState.AddModelError("Error", "Passwords do not match!");
                return BadRequest(ModelState);
            }

            string email = registrationData.Email;

            if(await _employerServices.GetEmployerByEmailasync(email) != null || await _jobseekerServices.GetJobSeekerByEmailAsync(email) != null)
            {
                ModelState.AddModelError("Error", "A Employer with the email already exists!");
                return BadRequest(ModelState);
            }

            var createdUser = await _accountServices.RegisterEmployerAsync(registrationData);

            if (createdUser != null)
            {
                return Ok(new
                {
                    success = true,
                    message = "Employer registered successfully.",
                    data = createdUser
                });
            }
            else
            {
                ModelState.AddModelError("Error", "An error occuerd while creating the employer!");
                return StatusCode(500, new
                {
                    success = false,
                    ModelState
                });
            }            
        }

        //JobSeeker Registration
        [HttpPost]
        [Route("JobSeeker/Register")]
        public async Task<ActionResult> RegisterJobSeeker([FromBody] RegisterJobSeekerDTO registrationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (registrationData.Password != registrationData.ConfirmPassword)
            {
                ModelState.AddModelError("Error", "Passwords do not match!");
                return BadRequest(ModelState);
            }

            string email = registrationData.Email;

            if (await _employerServices.GetEmployerByEmailasync(email) != null || await _jobseekerServices.GetJobSeekerByEmailAsync(email) != null)
            {
                ModelState.AddModelError("Error", "An JobSeeker with the email already exists!");
                return BadRequest(ModelState);
            }

            var createdUser = await _accountServices.RegisterJobSeekerAsync(registrationData);

            if (createdUser != null)
            {
                return Ok(new
                {
                    success = true,
                    message = "JobSeeker registered successfully.",
                    data = createdUser
                });
            }
            else
            {
                ModelState.AddModelError("Error", "An error occuerd while creating the jobseeker!");
                return StatusCode(500, new
                {
                    success = false,
                    ModelState
                });
            }
        }

       
        //Employer Login

        [HttpPost]
        [Route("Employer/Login")]
        public async Task<ActionResult> EmployerLogin([FromBody] LoginDTO loginCredentials)
        {
            if (loginCredentials == null) return BadRequest(ModelState); ;

            var user = await _employerServices.GetEmployerByEmailasync(loginCredentials.Email);

            if (user == null) return BadRequest("Invalid Email or Password!");

            var match = _authServices.VerifyPassword(loginCredentials.Password, user.Password);

            if (!match) return BadRequest("Invalid Email or Password!");

            var token = _accountServices.LoginAsync(user);
            user.Token = token;
            user.Password = null;


            try
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                };

                Response.Cookies.Append("token", token, cookieOptions);

                return Ok(new
                {
                    success = true,
                    token,
                    user,
                    message = "Logged in successfully"
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        //JobSeeker Login
        [HttpPost]
        [Route("JobSeeker/Login")]
        public async Task<ActionResult> JobSeekerLogin([FromBody] LoginDTO loginCredentials)
        {
            if (loginCredentials == null) return BadRequest(ModelState); ;

            var user = await _jobseekerServices.GetJobSeekerByEmailAsync(loginCredentials.Email);

            if (user == null) return BadRequest("Invalid Email or Password!");

            var match = _authServices.VerifyPassword(loginCredentials.Password, user.Password);

            if (!match) return BadRequest("Invalid Email or Password!");

            var token = _accountServices.LoginAsync(user);
            user.Token = token;
            user.Password = null;


            try
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                };

                Response.Cookies.Append("token", token, cookieOptions);

                return Ok(new
                {
                    success = true,
                    token,
                    user,
                    message = "Logged in successfully"
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout([FromServices] ITokenBlacklistService tokenBlacklistService)
        {
            string token = HttpContext.Request?.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { message = "Token is missing in the request header" });
            }
            DateTime expiryTime = DateTime.UtcNow.AddMinutes(30);

            tokenBlacklistService.AddTokenToBlacklist(token, expiryTime);

            return Ok(new { message = "Logout successful" });
        }

        //forget-password mail
        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPasswordAsync([FromBody] ForgetPasswordDTO model)
        {
            dynamic user;
            if (model == null)
            {
                return BadRequest();
            }

            //if (model.Role == "Employer")
            //{
            //    user = await _employerServices.GetEmployerByEmailasync(model.Email);
            //}
            //else
            //{
            //    user = await _jobseekerServices.GetJobSeekerByEmailAsync(model.Email);
            //}
            var users = await _employerServices.GetAllEmployersAsync();
            user = users.Where(u => u.Email == model.Email).FirstOrDefault();
            if (user == null)
            {
                var admins = await _jobseekerServices.GetAllJobSeekersAsync();
                user = admins.Where(u => u.Email == model.Email).FirstOrDefault();
            }

            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"User with email : {model.Email} does not exist"
                });
            }

            var data = await _accountServices.ForgotPassAsync(user);

            var origin = Request.Headers["Origin"];
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/reset-password/{data.Token}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 6 hours:</p>
                            <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
            {
                var resetUrl = $"http://localhost:4200/reset-password/{data.Token}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 6 hours:</p>
                            <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }

            var toEmail = model.Email;
            var subject = "Reset Password";
            var body = message;

            var isEmailSent = _emailService.SendEmailAsync(toEmail, subject, body);
            if (isEmailSent)
            {
                return Ok(new
                {
                    success = true,
                    message = "Email sent successfully",
                    user = data
                });
            }
            else
            {
                return BadRequest("Failed to send email");
            }
        }


        //reset-password 
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDTO model)
        {

            if (model == null)
            {
                return BadRequest();
            }
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest(new { success = false, message = "Password mismatch" });
            }

            dynamic user = await _accountServices.ResetPassAsync(model);

            if (user == null)
            {
                return BadRequest(new { success = false, message = "Token is expired. Please regenerate your token" });
            }

            string message = $@"<h1>Password Reset Successfull</h1><p>Your password has been changed successfully.</p>";



            var toEmail = model.Email;
            var subject = "Reset Password";
            var body = message;

            var isEmailSent = _emailService.SendEmailAsync(toEmail, subject, body);
            if (isEmailSent)
            {
                return Ok(new
                {
                    success = true,
                    message = "Password changed successfully.",
                    data = user
                });
            }
            else
            {
                return BadRequest("Failed to reset password");
            }
        }
    }
}
