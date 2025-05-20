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
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly IHasPermissionService _hasPermissionService;

        public PermissionController(IPermissionService permissionService,
            IHasPermissionService hasPermissionService)
        {
            _permissionService = permissionService;
            _hasPermissionService = hasPermissionService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreatePermissionDto createPermissionDto, CancellationToken cancel)
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
                    var createdPermissionViewModel = await _permissionService.CreateAsync(createPermissionDto, cancel);
                    return CreatedAtAction(nameof(FindOne), new { id = createdPermissionViewModel.Id }, createdPermissionViewModel);
                }
                else
                {
                    return Forbid("You do not have permission to create a permission.");
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
        public async Task<IActionResult> FindAll(CancellationToken cancel)
        {
            try
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Read, cancel))
                {
                    return Ok(await _permissionService.FindAllAsync(cancel));
                }
                else
                {
                    return Forbid("You do not have permission to read permissions.");
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
                    return Ok(await _permissionService.FindOneAsync(id, cancel));
                }
                else
                {
                    return Forbid("You do not have permission to read this resource.");
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
        public async Task<IActionResult?> Update([FromBody] UpdatePermissionDto udatePermissionDto, CancellationToken cancel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Edit, cancel))
                {
                    return Ok(await _permissionService.UpdateAsync(udatePermissionDto, cancel));
                }
                else
                {
                    return Forbid("You do not have permission to update a permission.");
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
                    return Ok(await _permissionService.SoftDelete(id, cancel));
                }
                else
                {
                    return Forbid("You do not have permission to delete a permission.");
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
                    return Ok(await _permissionService.RemoveAsync(id, cancel));
                }
                else
                {
                    return Forbid("You do not have permission to delete a permission.");
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
