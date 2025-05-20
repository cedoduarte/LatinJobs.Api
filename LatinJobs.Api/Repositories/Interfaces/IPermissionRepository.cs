using LatinJobs.Api.Entities;

namespace LatinJobs.Api.Repositories.Interfaces
{
    public interface IPermissionRepository
    {
        Task<Permission> CreateAsync(Permission permission, CancellationToken cancel = default);
        Task<IEnumerable<Permission>> FindAllAsync(CancellationToken cancel = default);
        Task<Permission?> FindOneAsync(int id, CancellationToken cancel = default);
        Task<Permission?> UpdateAsync(Permission permission, CancellationToken cancel = default);
        Task<Permission?> SoftDelete(int id, CancellationToken cancel = default);
        Task<Permission?> RemoveAsync(int id, CancellationToken cancel = default);
    }
}
