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
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IHasPermissionService _hasPermissionService;

        public RoleController(IRoleService roleService,
            IHasPermissionService hasPermissionService)
        {
            _roleService = roleService;
            _hasPermissionService = hasPermissionService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateRoleDto createRoleDto, CancellationToken cancel)
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
                    var createdRoleViewModel = await _roleService.CreateAsync(createRoleDto, cancel);
                    return CreatedAtAction(nameof(FindOne), new { id = createdRoleViewModel.Id }, createdRoleViewModel);
                }
                else
                {
                    return Forbid("You do not have permission to create a role.");
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
                    return Ok(await _roleService.FindAllAsync(cancel));
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
                    return Ok(await _roleService.FindOneAsync(id, cancel));
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

        [HttpGet("name/{name}")]
        [Authorize]
        public async Task<IActionResult> FindOne([FromRoute] string name, CancellationToken cancel)
        {
            try
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Read, cancel))
                {
                    return Ok(await _roleService.FindOneAsync(name, cancel));
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
        public async Task<IActionResult> Update([FromBody] UpdateRoleDto updateRoleDto, CancellationToken cancel)
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
                    return Ok(await _roleService.UpdateAsync(updateRoleDto, cancel));
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
                    return Ok(await _roleService.SoftDeleteAsync(id, cancel));
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
        public async Task<IActionResult> Remove([FromRoute] int id, CancellationToken cancel)
        {
            try
            {
                if (await _hasPermissionService.HasPermissionAsync(
                    User.FindFirst(Constants.Jwt.UserIdClaim)!.Value,
                    Constants.Permissions.Delete, cancel))
                {
                    return Ok(await _roleService.RemoveAsync(id, cancel));
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
