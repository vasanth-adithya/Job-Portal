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
    public class ResumeController : ControllerBase
    {
        private readonly ILogger<ResumeController> _logger;
        private readonly IResumeServices _resumeServices;
        public ResumeController(ILogger<ResumeController> logger, IResumeServices resumeServices)
        {
            _logger = logger;
            _resumeServices = resumeServices;

        }

        //GetAll
        [Authorize(Roles = "Employer,JobSeeker")]
        [HttpGet]
        [Route("GetAllResumes")]
        public async Task<ActionResult<List<Resume>>> GetAllResumesAsync()
        {
            try
            {
                var resumes = await _resumeServices.GetAllResumesAsync();

                if (resumes == null || resumes.Count <= 0)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "No data found"
                    });
                }

                return Ok(resumes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Resumes."
                });
            }
        }

        //GetResumeById
        [Authorize(Roles = "Employer,JobSeeker")]
        [HttpGet]
        [Route("GetResumeById/{id}", Name = "GetResumeById")]
        public async Task<ActionResult<Resume>> GetResumeByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Resume Id"
                    });
                }

                var resume = await _resumeServices.GetResumeByIdAsync(id);

                if (resume == null)
                {
                    _logger.LogError("Resume not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Resume' with Id: {id} not found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = resume
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Resume."
                });
            }
        }

        [Authorize(Roles = "Employer,JobSeeker")]
        [HttpGet]
        [Route("GetResumeByJSId/{id}", Name = "GetResumeByJSId")]
        public async Task<ActionResult<Resume>> GetResumeByJSIdAsync(int id)
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

                var resumes = await _resumeServices.GetResumeByJSIdAsync(id);

                if (resumes.Count == 0)
                {
                    _logger.LogError("Resumes not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Resumes' with Id: {id} not found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = resumes
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Resumes."
                });
            }
        }


        //CreateResume
        [Authorize(Roles = "JobSeeker")]
        [HttpPost]
        [Route("CreateResume")]
        public async Task<ActionResult<Resume>> CreateResumeAsync([FromBody] Resume model)
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
                var createdResume = await _resumeServices.CreateResumeAsync(model);
                return Ok(new
                {
                    success = true,
                    data = createdResume
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating Resume."
                });
            }
        }

        //UpdateResume
        [Authorize(Roles = "JobSeeker")]
        [HttpPut]
        [Route("UpdateResume")]
        public async Task<ActionResult<bool>> UpdateResumeAsync([FromBody] Resume model)
        {
            try
            {
                if (model == null || model.ResumeId <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var resumeUpd = await _resumeServices.UpdateResumeAsync(model);

                if (resumeUpd == false)
                {
                    _logger.LogError("Resume not found with given Id");
                    return BadRequest(new
                    {
                        success = false,
                        message = $"Resume not found with given Id: {model.ResumeId} "
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Resume updated successfully"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating Resume."
                });
            }
        }

        //DeleteResume
        [Authorize(Roles = "JobSeeker")]
        [HttpDelete]
        [Route("DeleteResume/{id}")]
        public async Task<ActionResult<bool>> DeleteResumeAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Resume Id"
                    });
                }

                var resume = await _resumeServices.DeleteResumeAsync(id);
                if (resume == false)
                {
                    _logger.LogError("Resume not found with given Id");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Resume Id"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Resume deleted successfully"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting Resume."
                });
            }
        }
    }
}
