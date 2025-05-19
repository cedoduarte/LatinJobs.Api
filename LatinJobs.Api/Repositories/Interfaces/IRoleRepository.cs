using LatinJobs.Api.Entities;

namespace LatinJobs.Api.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> CreateAsync(Role role, CancellationToken cancel = default);
        Task<IEnumerable<Role>> FindAllAsync(CancellationToken cancel = default);
        Task<Role?> FindOneAsync(int id, CancellationToken cancel = default);
        Task<Role?> UpdateAsync(Role role, CancellationToken cancel = default);
        Task<Role?> RemoveAsync(int id, CancellationToken cancel = default);
    }
}
