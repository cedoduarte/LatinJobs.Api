using LatinJobs.Api.Entities;

namespace LatinJobs.Api.Repositories.Interfaces
{
    public interface IRolePermissionRepository
    {
        Task<RolePermission> CreateAsync(RolePermission rolePermission, CancellationToken cancel = default);
        Task<IEnumerable<RolePermission>> FindAllAsync(CancellationToken cancel = default);
        Task<RolePermission?> FindOneAsync(int id, CancellationToken cancel = default);
        Task<RolePermission?> UpdateAsync(RolePermission rolePermission, CancellationToken cancel = default);
        Task<RolePermission?> RemoveAsync(int id, CancellationToken cancel = default);
    }
}
