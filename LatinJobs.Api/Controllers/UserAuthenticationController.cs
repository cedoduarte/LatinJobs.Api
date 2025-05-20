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
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IHasPermissionService _hasPermissionService;

        public UserAuthenticationController(
            IUserAuthenticationService userAuthenticationService, 
            IHasPermissionService hasPermissionService)
        {
            _userAuthenticationService = userAuthenticationService;
            _hasPermissionService = hasPermissionService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserAuthenticationDto createAuthenticationDto, CancellationToken cancel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdUserAuthenticationViewModel = await _userAuthenticationService.CreateAsync(createAuthenticationDto, cancel);
                return CreatedAtAction(nameof(FindByUserId), new { userId = createdUserAuthenticationViewModel.UserId }, createdUserAuthenticationViewModel);
            }
            catch (UnauthorizedException ex)
            {
                return Forbid(ex.Message);
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
                    return Ok(await _userAuthenticationService.FindAllAsync(cancel));
                }
                else
                {
                    return Forbid("You do not have permission to view user authentications.");
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
        public async Task<IActionResult> FindByUserId([FromRoute] int userId, CancellationToken cancel)
        {
            try 
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Read, cancel))
                {
                    return Ok(await _userAuthenticationService.FindByUserIdAsync(userId, cancel));
                }
                else
                {
                    return Forbid("You do not have permission to view user authentications.");
                }
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 $"An unexpected error occurred. Please try again later. {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> RemoveAsync([FromRoute] int id, CancellationToken cancel)
        {
            try 
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Delete, cancel))
                {
                    return Ok(await _userAuthenticationService.RemoveAsync(id, cancel));
                }
                else
                {
                    return Forbid("You do not have permission to delete this resource.");
                }
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 $"An unexpected error occurred. Please try again later. {ex.Message}");
            }
        }
    }
}
