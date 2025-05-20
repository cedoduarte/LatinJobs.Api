using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;
using LatinJobs.Api.ViewModels;

namespace LatinJobs.Api.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<PermissionViewModel> CreateAsync(CreatePermissionDto createPermissionDto, CancellationToken cancel = default);
        Task<IEnumerable<PermissionViewModel>> FindAllAsync(CancellationToken cancel = default);
        Task<PermissionViewModel?> FindOneAsync(int id, CancellationToken cancel = default);
        Task<PermissionViewModel?> UpdateAsync(UpdatePermissionDto udatePermissionDto, CancellationToken cancel = default);
        Task<PermissionViewModel?> SoftDelete(int id, CancellationToken cancel = default);
        Task<PermissionViewModel?> RemoveAsync(int id, CancellationToken cancel = default);
    }
}
