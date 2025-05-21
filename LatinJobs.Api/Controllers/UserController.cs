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
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHasPermissionService _hasPermissionService;

        public UserController(IUserService userService,
            IHasPermissionService hasPermissionService)
        {
            _userService = userService;
            _hasPermissionService = hasPermissionService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto, CancellationToken cancel) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdUserViewModel = await _userService.CreateAsync(createUserDto, cancel);
                return CreatedAtAction(nameof(FindOne), new { id = createdUserViewModel.Id }, createdUserViewModel);
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
        public async Task<IActionResult> FindAll([FromQuery] PaginationParametersDto paginationParametersDto, CancellationToken cancel)
        {
            try 
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Read, cancel))
                {
                    return Ok(await _userService.FindAllAsync(paginationParametersDto, cancel));
                }
                else
                {
                    return Forbid("You do not have permission to read this resource.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"An unexpected error occurred. Please try again later. {ex.Message}");
            }
        }

        [HttpGet("find-by-id/{id}")]
        [Authorize]
        public async Task<IActionResult> FindOne([FromRoute] int id, CancellationToken cancel)
        {
            try
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Read, cancel))
                {
                    return Ok(await _userService.FindOneAsync(id, cancel));
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

        [HttpGet("find-by-email/{email}")]
        [Authorize]
        public async Task<IActionResult> FindOneByEmail([FromRoute] string email, CancellationToken cancel)
        {
            try
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Read, cancel))
                {
                    return Ok(await _userService.FindOneByEmailAsync(email, cancel));
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
        public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto, CancellationToken cancel)
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
                    return Ok(await _userService.UpdateAsync(updateUserDto, cancel));
                }
                else
                {
                    return Forbid("You do not have permission to update this resource.");
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
                    return Ok(await _userService.SoftDeleteAsync(id, cancel));
                }
                else
                {
                    return Forbid("You do not have permission to delete this resource.");
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
        public async Task<IActionResult?> Remove([FromRoute] int id, CancellationToken cancel)
        {
            try
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Delete, cancel))
                {
                    return Ok(await _userService.RemoveAsync(id, cancel));
                }
                else
                {
                    return Forbid("You do not have permission to delete this resource.");
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
