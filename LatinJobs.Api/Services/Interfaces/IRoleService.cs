using LatinJobs.Api.DTOs;
using LatinJobs.Api.ViewModels;

namespace LatinJobs.Api.Services.Interfaces
{
    public interface IRoleService
    {
        Task<RoleViewModel> CreateAsync(CreateRoleDto createRoleDto, CancellationToken cancel = default);
        Task<IEnumerable<RoleViewModel>> FindAllAsync(CancellationToken cancel = default);
        Task<RoleViewModel?> FindOneAsync(int id, CancellationToken cancel = default);
        Task<RoleViewModel?> FindOneAsync(string name, CancellationToken cancel = default);
        Task<RoleViewModel?> UpdateAsync(UpdateRoleDto updateRoleDto, CancellationToken cancel = default);
        Task<RoleViewModel?> SoftDeleteAsync(int id, CancellationToken cancel = default);
        Task<RoleViewModel?> RemoveAsync(int id, CancellationToken cancel = default);
    }
}
