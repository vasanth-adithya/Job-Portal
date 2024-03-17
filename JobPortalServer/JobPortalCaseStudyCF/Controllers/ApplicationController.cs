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
    public class ApplicationController : ControllerBase
    {
        private readonly ILogger<ApplicationController> _logger;
        private readonly IApplicationServices _applicationServices;
        public ApplicationController(ILogger<ApplicationController> logger, IApplicationServices applicationServices)
        {
            _logger = logger;
            _applicationServices = applicationServices;

        }

        //GetAll
        [Authorize(Roles = "Employer,JobSeeker")]
        [HttpGet]
        [Route("GetAllApplications")]
        public async Task<ActionResult<List<Application>>> GetAllApplicationsAsync()
        {
            try
            {
                var applications = await _applicationServices.GetAllApplicationsAsync();

                if (applications == null || applications.Count <= 0)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "No data found"
                    });
                }

                return Ok(applications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Applications."
                });
            }
        }

        //GetApplicationById
        [Authorize(Roles = "Employer,JobSeeker")]
        [HttpGet]
        [Route("GetApplicationById/{id}", Name = "GetApplicationById")]
        public async Task<ActionResult<Application>> GetApplicationByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Application Id"
                    });
                }

                var application = await _applicationServices.GetApplicationByIdAsync(id);

                if (application == null)
                {
                    _logger.LogError("Application not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Application' with Id: {id} not found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = application
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

        [Authorize(Roles = "JobSeeker")]
        [HttpGet]
        [Route("GetApplicationByJSId/{id}", Name = "GetApplicationByJSId")]
        public async Task<ActionResult<Application>> GetApplicationByJSIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid JobSeeker Id"
                    });
                }

                var application = await _applicationServices.GetApplicationByJSIdAsync(id);

                if (application.Count==0)
                {
                    _logger.LogError("Application not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Application' with Id: {id} not found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = application
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Application."
                });
            }
        }

        [Authorize(Roles = "Employer")]
        [HttpGet]
        [Route("GetApplicationByEmployerId/{id}")]
        public async Task<ActionResult<Application>> GetApplicationByEmployerIDAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Listing Id"
                    });
                }

                var application = await _applicationServices.GetApplicationByEmployerIDAsync(id);
                //application = application.Where(a=>a.Listing.EmployerId==id).ToList();
                
                if (application != null && application.Count() == 0)
                {
                    _logger.LogError("Application not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Application' with Id: {id} not found"
                    });
                }
                else if(application== null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Application' with Id: {id} not found"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = true,
                        data = application
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Application."
                });
            }
        }

        [Authorize(Roles = "Employer,JobSeeker")]
        [HttpGet]
        [Route("GetApplicationByListingId/{id}", Name = "GetApplicationByListingId")]
        public async Task<ActionResult<Application>> GetApplicationByListingIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Listing Id"
                    });
                }

                var application = await _applicationServices.GetApplicationByListingIdAsync(id);

                if (application.Count == 0)
                {
                    _logger.LogError("Application not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Application' with Id: {id} not found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = application
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Application."
                });
            }
        }

        //CreateApplication
        [Authorize(Roles = "JobSeeker")]
        [HttpPost]
        [Route("CreateApplication")]
        public async Task<ActionResult<Application>> CreateApplicationAsync([FromBody] Application model)
        {
            try
            {
                if (model == null)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Null Object"
                    });
                }
                var application = await _applicationServices.GetApplicationByJSIdAsync(model.JobSeekerId);
                if (application != null && application.Any(a => a.ListingId == model.ListingId))
                {
                    return StatusCode(400, "You have already applied to the Job");
                }
                var createdApplication = await _applicationServices.CreateApplicationAsync(model);
                return Ok(new
                {
                    success = true,
                    data = createdApplication
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating Application."
                });
            }
        }

        //UpdateApplication
        [Authorize(Roles = "Employer,JobSeeker")]
        [HttpPut]
        [Route("UpdateApplication")]
        public async Task<ActionResult<bool>> UpdateApplicationAsync([FromBody] Application model)
        {
            try
            {
                if (model == null || model.ApplicationId <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var applicationupd = await _applicationServices.UpdateApplicationAsync(model);

                if (applicationupd == false)
                {
                    _logger.LogError("Application not found with given Id");
                    return BadRequest(new
                    {
                        success = false,
                        message = $"Application not found with given Id: {model.ApplicationId} "
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Application updated successfully"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating Application."
                });
            }
        }

        //DeleteApplication
        [Authorize(Roles = "JobSeeker")]
        [HttpDelete]
        [Route("DeleteApplication/{id}")]
        public async Task<ActionResult<bool>> DeleteApplicationAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Application Id"
                    });
                }

                var application = await _applicationServices.DeleteApplicationAsync(id);
                if (application == false)
                {
                    _logger.LogError("Application not found with given Id");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Application Id"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Application deleted successfully"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting Application."
                });
            }
        }
    }
}
