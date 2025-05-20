using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;
using LatinJobs.Api.ViewModels;

namespace LatinJobs.Api.Services.Interfaces
{
    public interface IRolePermissionService
    {
        Task<RolePermissionViewModel> CreateAsync(CreateRolePermissionDto createRolePermissionDto, CancellationToken cancel = default);
        Task<IEnumerable<RolePermissionViewModel>> FindAllAsync(CancellationToken cancel = default);
        Task<IEnumerable<PermissionViewModel>> GetPermissions(int roleId, CancellationToken cancel = default);
        Task<RolePermissionViewModel> RemoveAsync(int roleId, int permissionId, CancellationToken cancel = default);
    }
}
