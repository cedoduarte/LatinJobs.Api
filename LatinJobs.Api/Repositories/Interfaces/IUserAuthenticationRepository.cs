using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;

namespace LatinJobs.Api.Repositories.Interfaces
{
    public interface IUserAuthenticationRepository
    {
        Task<UserAuthentication> CreateAsync(UserAuthentication authentication, CancellationToken cancel = default);
        Task<IEnumerable<UserAuthentication>> FindAllAsync(CancellationToken cancel = default);
        Task<IEnumerable<UserAuthentication>> FindByUserIdAsync(int userId, CancellationToken cancel = default);
        Task<UserAuthentication?> RemoveAsync(int id, CancellationToken cancel = default);
    }
}
