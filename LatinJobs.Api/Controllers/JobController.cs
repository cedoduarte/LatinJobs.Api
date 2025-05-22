using LatinJobs.Api.DTOs;
using LatinJobs.Api.Exceptions;
using LatinJobs.Api.Services.Interfaces;
using LatinJobs.Api.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LatinJobs.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : Controller
    {
        private readonly IJobService _jobService;
        private readonly IHasPermissionService _hasPermissionService;

        public JobController(IJobService jobService,
            IHasPermissionService hasPermissionService)
        {
            _jobService = jobService;
            _hasPermissionService = hasPermissionService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateJobDto createJobDto, CancellationToken cancel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try 
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Write, cancel))
                {
                    var createdJobViewModel = await _jobService.CreateAsync(createJobDto, cancel);
                    return CreatedAtAction(nameof(FindOne), new { id = createdJobViewModel.Id }, createdJobViewModel);
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to create a job.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 $"An unexpected error occurred. Please try again later. {ex.Message}");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> FindAll([FromQuery] PaginationParametersDto paginationParametersDto, CancellationToken cancel)
        {
            try 
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Read, cancel))
                {
                    return Ok(await _jobService.FindAllAsync(paginationParametersDto, cancel));
                }
                else 
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to read jobs.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 $"An unexpected error occurred. Please try again later. {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> FindOne([FromRoute] int id, CancellationToken cancel)
        {
            try 
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Read, cancel))
                { 
                    return Ok(await _jobService.FindOneAsync(id, cancel));
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to read this job.");
                }
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 $"An unexpected error occurred. Please try again later. {ex.Message}");
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateJobDto updateJobDto, CancellationToken cancel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try 
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Write, cancel))
                {
                    var updatedJobViewModel = await _jobService.UpdateAsync(updateJobDto, cancel);
                    return Ok(updatedJobViewModel);
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to update this job.");
                }
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An unexpected error occurred. Please try again later. {ex.Message}");
            }
        }

        [HttpDelete("soft/{id}")]
        [Authorize]
        public async Task<IActionResult> SoftDelete([FromRoute] int id, CancellationToken cancel)
        {
            try
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Delete, cancel))
                {
                    return Ok(await _jobService.SoftDeleteAsync(id, cancel));
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to delete this job.");
                }
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An unexpected error occurred. Please try again later. {ex.Message}");
            }
        }

        [HttpDelete("hard/{id}")]
        [Authorize]
        public async Task<IActionResult> Remove([FromRoute] int id, CancellationToken cancel)
        {
            try
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Delete, cancel))
                {
                    return Ok(await _jobService.RemoveAsync(id, cancel));
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to delete this job.");
                }
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An unexpected error occurred. Please try again later. {ex.Message}");
            }
        }
    }
}
