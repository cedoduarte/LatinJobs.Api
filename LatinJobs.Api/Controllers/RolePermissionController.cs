using LatinJobs.Api.DTOs;
using LatinJobs.Api.Exceptions;
using LatinJobs.Api.Services.Interfaces;
using LatinJobs.Api.Shared;
using LatinJobs.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LatinJobs.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolePermissionController : Controller
    {
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IHasPermissionService _hasPermissionService;

        public RolePermissionController(
            IRolePermissionService rolePermissionService,
            IHasPermissionService hasPermissionService)
        {
            _rolePermissionService = rolePermissionService;
            _hasPermissionService = hasPermissionService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateRolePermissionDto createRolePermissionDto, CancellationToken cancel)
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
                    var createdRolePermission = await _rolePermissionService.CreateAsync(createRolePermissionDto, cancel);
                    return CreatedAtAction(nameof(FindAll), new { id = createdRolePermission.Id }, createdRolePermission);
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to create a role permission.");
                }
            }
            catch (AlreadyExistsException ex)
            {
                return Conflict(ex.Message);
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
                    return Ok(await _rolePermissionService.FindAllAsync(cancel));
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to view role permissions.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"An unexpected error occurred. Please try again later. {ex.Message}");
            }
        }

        [HttpGet("{roleId}")]
        [Authorize]
        public async Task<IActionResult> GetPermissions([FromRoute] int roleId, CancellationToken cancel)
        {
            try
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Read, cancel))
                {
                    return Ok(await _rolePermissionService.GetPermissions(roleId, cancel));
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to view role permissions.");
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

        [HttpDelete("role/{roleId}/permission/{permissionId}")]
        [Authorize]
        public async Task<IActionResult> Remove([FromRoute] int roleId, [FromRoute] int permissionId, CancellationToken cancel)
        {
            try
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Delete, cancel))
                {
                    return Ok(await _rolePermissionService.RemoveAsync(roleId, permissionId, cancel));
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "User does not have permission to delete this role permission.");
                }
            }
            catch (NotFoundException)
            {
                return NotFound($"Role with ID {roleId} or Permission with Id {permissionId} Not Found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An unexpected error occurred. Please try again later. {ex.Message}");
            }
        }
    }
}