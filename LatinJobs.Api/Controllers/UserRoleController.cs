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
    public class UserRoleController : Controller
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IHasPermissionService _hasPermissionService;

        public UserRoleController(IUserRoleService userRoleService,
            IHasPermissionService hasPermissionService)
        {
            _userRoleService = userRoleService;
            _hasPermissionService = hasPermissionService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateUserRoleDto createUserRoleDto, CancellationToken cancel)
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
                    var createdUserRoleViewModel = await _userRoleService.CreateAsync(createUserRoleDto, cancel);
                    return CreatedAtAction(nameof(FindOneByUserId), new { userId = createdUserRoleViewModel.User!.Id }, createdUserRoleViewModel);
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to create a user role.");
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
                    return Ok(await _userRoleService.FindAllAsync(cancel));
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to read user roles.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 $"An unexpected error occurred. Please try again later. {ex.Message}");
            }
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult?> FindOneByUserId([FromRoute] int userId, CancellationToken cancel)
        {
            try
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Read))
                {
                    return Ok(await _userRoleService.FindOneByUserIdAsync(userId, cancel));
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to read this user role.");
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
        public async Task<IActionResult> Update([FromBody] UpdateUserRoleDto updateUserRoleDto, CancellationToken cancel)
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
                    return Ok(await _userRoleService.UpdateAsync(updateUserRoleDto, cancel));
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to update this user role.");
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

        [HttpDelete("{userId}")]
        [Authorize]
        public async Task<IActionResult> RemoveByUserId([FromRoute] int userId, CancellationToken cancel)
        {
            try
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Delete, cancel))
                {
                    return Ok(await _userRoleService.RemoveByUserIdAsync(userId, cancel));
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to delete this user role.");
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
