using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Models;
using JobPortalCaseStudyCF.Repositories;
using JobPortalCaseStudyCF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalCaseStudyCF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobListingController : ControllerBase
    {
        private readonly ILogger<JobListingController> _logger;
        private readonly IJobListingServices _jobListingServices;
        public JobListingController(ILogger<JobListingController> logger, IJobListingServices jobListingServices)
        {
            _logger = logger;
            _jobListingServices = jobListingServices;

        }

        //GetAll
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllJobListings")]
        public async Task<ActionResult<List<JobListing>>> GetAllJobListingsAsync()
        {
            try
            {
                var jobListings = await _jobListingServices.GetAllJobListingsAsync();

                if (jobListings == null || jobListings.Count <= 0)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "No data found"
                    });
                }

                return Ok(jobListings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Job Listings."
                });
            }
        }

        //GetJobListingsById
        //[Authorize(Roles = "Employer,JobSeeker")]
        [AllowAnonymous]
        [HttpGet]
        [Route("GetJobListingsById/{id}", Name = "GetJobListingsById")]
        public async Task<ActionResult<JobListing>> GetJobListingsByIdAsync(int id)
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

                var jobListing = await _jobListingServices.GetJobListingByIdAsync(id);

                if (jobListing == null)
                {
                    _logger.LogError("Listing not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Listing' with Id: {id} not found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = jobListing
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Listing."
                });
            }
        }
        [Authorize(Roles = "Employer")]
        [HttpGet]
        [Route("GetJobListingsByEmployerId/{id}", Name = "GetJobListingsByEmployerId")]
        public async Task<ActionResult<JobListing>> GetJobListingByEmployerIdAsync(int id)
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

                var jobListings = await _jobListingServices.GetJobListingByEmployerIdAsync(id);

                if (jobListings.Count == 0)
                {
                    _logger.LogError("JobListings not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'JobListings' with Id: {id} not found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = jobListings
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching JobListings."
                });
            }
        }


        //CreateJobListing
        [Authorize(Roles = "Employer")]
        [HttpPost]
        [Route("CreateJobListing")]
        public async Task<ActionResult<JobListing>> CreateJobListingAsync([FromBody] JobListing model)
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
                var createdListing = await _jobListingServices.CreateJobListingAsync(model);
                return Ok(new
                {
                    success = true,
                    data = createdListing
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating Listing."
                });
            }
        }

        //UpdateJobListing
        [Authorize(Roles = "Employer")]
        [HttpPut]
        [Route("UpdateJobListing")]
        public async Task<ActionResult<bool>> UpdateJobListingAsync([FromBody] JobListing model)
        {
            try
            {
                if (model == null || model.ListingId <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var listingUpd = await _jobListingServices.UpdateJobListingAsync(model);

                if (listingUpd == false)
                {
                    _logger.LogError("Listing not found with given Id");
                    return BadRequest(new
                    {
                        success = false,
                        message = $"Listing not found with given Id: {model.ListingId} "
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Listing updated successfully"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating Listing."
                });
            }
        }

        //DeleteJobListing
        [Authorize(Roles = "Employer")]
        [HttpDelete]
        [Route("DeleteJobListing/{id}")]
        public async Task<ActionResult<bool>> DeleteJobListingAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Lisitng Id"
                    });
                }

                var jobListing = await _jobListingServices.DeleteJobListingAsync(id);
                if (jobListing == false)
                {
                    _logger.LogError("Listing not found with given Id");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Listing Id"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Listing deleted successfully"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting Listing."
                });
            }
        }
    }
}
