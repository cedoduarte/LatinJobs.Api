using LatinJobs.Api.Entities;

namespace LatinJobs.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user, CancellationToken cancel = default);
        Task<IEnumerable<User>> FindAllAsync(CancellationToken cancel = default);
        Task<User?> FindOneAsync(int id, CancellationToken cancel = default);
        Task<User?> UpdateAsync(User user, CancellationToken cancel = default);
        Task<User?> RemoveAsync(int id, CancellationToken cancel = default);
    }
}
