using JobPortalCaseStudyCF.Interfaces;
using JobPortalCaseStudyCF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalCaseStudyCF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSeekerController : ControllerBase
    {
        private readonly IJobSeekerServices _jobseekerServices;
        private readonly ILogger<JobSeekerController> _logger;

        public JobSeekerController(ILogger<JobSeekerController> logger, IJobSeekerServices jobseekerServices)
        {
            _logger = logger;
            _jobseekerServices = jobseekerServices;
        }


        //GetAll
        //[Authorize(Roles ="JobSeeker")]
        [HttpGet]
        [Route("GetAllJobSeekers")]
        public async Task<ActionResult<List<JobSeeker>>> GetAllJobSeekersAsync()
        {
            try
            {
                var jobSeekers = await _jobseekerServices.GetAllJobSeekersAsync();

                if (jobSeekers == null || jobSeekers.Count <= 0)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "No data found"
                    });
                }

                return Ok(jobSeekers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching JobSeekers."
                });
            }
        }

        [Authorize(Roles = "Employer,JobSeeker")]
        [HttpGet]
        [Route("GetJobSeekerById/{id}", Name = "GetJobSeekerById")]
        public async Task<ActionResult<JobSeeker>> GetJobSeekerByIdAsync(int id)
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
                var jobSeeker = await _jobseekerServices.GetJobSeekerByIdAsync(id);


                if (jobSeeker == null)
                {
                    _logger.LogError("JobSeeker not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'JobSeeker' with Id: {id} not found"
                    });
                }

                return Ok(jobSeeker);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching JobSeeker."
                });
            }
        }


        //GetJobSeekerByName
        [Authorize(Roles = "Employer,JobSeeker")]
        [HttpGet]
        [Route("GetJobSeekerByEmail/{email}")]
        public async Task<ActionResult<JobSeeker>> GetJobSeekerByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid JobSeeker Email"
                    });
                }

                var jobSeeker = await _jobseekerServices.GetJobSeekerByEmailAsync(email);

                if (jobSeeker == null)
                {
                    _logger.LogError("JobSeeker not found with given name");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'JobSeeker' with Email: {email} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = jobSeeker
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching JobSeeker."
                });
            }
        }

        //CreateJobSeeker
        [Authorize(Roles = "JobSeeker")]
        [HttpPost]
        [Route("CreateJobSeeker")]
        public async Task<ActionResult<JobSeeker>> CreateJobSeekerAsync([FromBody] JobSeeker jobseekers)
        {
            try
            {
                if (jobseekers == null)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Null Object"
                    });
                }

                var createdJobSeeker = await _jobseekerServices.CreateJobSeekerAsync(jobseekers);

                return Ok(createdJobSeeker);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating JobSeeker."
                });
            }
        }


        //UpdateJobSeeker
        [Authorize(Roles = "JobSeeker")]
        [HttpPut]
        [Route("UpdateJobSeeker")]
        public async Task<ActionResult<bool>> UpdateJobSeekerAsync([FromBody] JobSeeker jobseekers)
        {
            try
            {
                if (jobseekers == null || jobseekers.JobSeekerId <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var jobSeeker = await _jobseekerServices.UpdateJobSeekerAsync(jobseekers, true);

                if (jobSeeker == false)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = $"JobSeeker not found with given Id: {jobseekers.JobSeekerId}"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = true,
                        message = "JobSeeker updated successfully",
                        data = jobseekers
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating JobSeeker."
                });
            }
        }




        //DeleteJobSeeker
        [Authorize(Roles = "JobSeeker")]
        [HttpDelete]
        [Route("DeleteJobSeeker/{id}")]
        public async Task<ActionResult<bool>> DeleteJobSeekerAsync(int id)
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

                var deleteStatus = await _jobseekerServices.DeleteJobSeekerAsync(id);

                //if (deleteStatus)
                //{
                //    return Ok(new
                //    {
                //        success = true,
                //        message = "JobSeeker Deleted Successfully"

                //});
                //}
                //else
                //{
                //    return NotFound(new
                //    {
                //        success = false,
                //        message = "JObsseker Not Found"
                //    });
                //}
                return Ok(deleteStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting JobSeeker."
                });
            }
        }
    }
}
