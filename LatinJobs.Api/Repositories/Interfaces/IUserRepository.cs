using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;
using LatinJobs.Api.Pagination;
using LatinJobs.Api.ViewModels;

namespace LatinJobs.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user, CancellationToken cancel = default);
        Task<PagedResult<UserViewModel>> FindAllAsync(PaginationParametersDto paginationParametersDto, CancellationToken cancel = default);
        Task<User?> FindOneAsync(int id, CancellationToken cancel = default);
        Task<User?> FindOneByEmail(string email, CancellationToken cancel = default);
        Task<User?> UpdateAsync(User user, CancellationToken cancel = default);
        Task<User?> SoftDelete(int id, CancellationToken cancel = default);
        Task<User?> RemoveAsync(int id, CancellationToken cancel = default);
    }
}
