using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Models;
using JobPortalCaseStudyCF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalCaseStudyCF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly ILogger<EmployerController> _logger;
        private readonly IEmployerServices _employerServices;

        public EmployerController(ILogger<EmployerController> logger, IEmployerServices employerServices)
        {
            _logger = logger;
            _employerServices = employerServices;
        }

        //GetAll
        [Authorize(Roles = "Employer")]
        [HttpGet]
        [Route("GetAllEmployers")]
        public async Task<ActionResult<List<Employer>>> GetAllEmployersAsync()
        {
            try
            {
                var employers = await _employerServices.GetAllEmployersAsync();

                if (employers == null || employers.Count <= 0)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "No data found"
                    });
                }

                return Ok(employers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Employers."
                });
            }
        }

        //GetEmployerById
        [Authorize(Roles = "Employer")]
        [HttpGet]
        [Route("GetEmployerById/{id}", Name = "GetEmployerById")]
        public async Task<ActionResult<Employer>> GetEmployerByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Employer Id"
                    });
                }
                var Employer = await _employerServices.GetEmployerByIdAsync(id);


                if (Employer == null)
                {
                    _logger.LogError("Employer not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Employer' with Id: {id} not found"
                    });
                }

                return Ok(Employer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Employer."
                });
            }
        }

        //GetEmployerByName
        [Authorize(Roles = "Employer")]
        [HttpGet]
        [Route("GetEmployerByEmail/{email}")]
        public async Task<ActionResult<Employer>> GetEmployerByEmailasync(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Employer email"
                    });
                }

                var employer = await _employerServices.GetEmployerByEmailasync(email);

                if (employer == null)
                {
                    _logger.LogError("Employer not found with given email");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Employer' with email: {email} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = employer
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Employer."
                });
            }
        }

        //CreateEmployer
        [Authorize(Roles = "Employer")]
        [HttpPost]
        [Route("CreateEmployer")]
        public async Task<ActionResult<Employer>> CreateEmployerAsync([FromBody] Employer employer)
        {
            try
            {
                if (employer == null)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Null Object"
                    });
                }

                var createdEmployer = await _employerServices.CreateEmployerAsync(employer);

                return Ok(createdEmployer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating Employer."
                });
            }
        }


        //UpdateEmployer
        [Authorize(Roles = "Employer")]
        [HttpPut]
        [Route("UpdateEmployer")]
        public async Task<ActionResult<bool>> UpdateEmployerAsync([FromBody] Employer employer)
        {
            try
            {
                if (employer == null || employer.EmployerId <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var Employer = await _employerServices.UpdateEmployerAsync(employer, true);

                if (Employer == false)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = $"Employer not found with given Id: {employer.EmployerId}"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Employer updated successfully",
                        data = employer
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating Employer."
                });
            }
        }


        //DeleteEmployer
        [Authorize(Roles = "Employer")]
        [HttpDelete]
        [Route("DeleteEmployer/{id}")]
        public async Task<ActionResult<bool>> DeleteEmployerAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Employer Id"
                    });
                }

                var deleteStatus = await _employerServices.DeleteEmployerAsync(id);

                if (deleteStatus == false)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Employer Id"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Employer deleted successfully"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting Employer."
                });
            }
        }

    }
}
