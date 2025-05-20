using LatinJobs.Api.DTOs;
using LatinJobs.Api.ViewModels;

namespace LatinJobs.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> CreateAsync(CreateUserDto createUserDto, CancellationToken cancel = default);
        Task<IEnumerable<UserViewModel>> FindAllAsync(CancellationToken cancel = default);
        Task<UserViewModel?> FindOneAsync(int id, CancellationToken cancel = default);
        Task<UserViewModel?> FindOneByEmailAsync(string email, CancellationToken cancel = default);

        Task<UserViewModel?> UpdateAsync(UpdateUserDto user, CancellationToken cancel = default);
        Task<UserViewModel?> SoftDeleteAsync(int id, CancellationToken cancel = default);
        Task<UserViewModel?> RemoveAsync(int id, CancellationToken cancel = default);
    }
}
