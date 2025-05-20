using LatinJobs.Api.DTOs;
using LatinJobs.Api.ViewModels;

namespace LatinJobs.Api.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<UserRoleViewModel> CreateAsync(CreateUserRoleDto createUserRoleDto, CancellationToken cancel = default);
        Task<IEnumerable<UserRoleViewModel>> FindAllAsync(CancellationToken cancel = default);
        Task<UserRoleViewModel?> FindOneByUserIdAsync(int userId, CancellationToken cancel = default);
        Task<UserRoleViewModel?> UpdateAsync(UpdateUserRoleDto updateUserRoleDto, CancellationToken cancel = default);
        Task<UserRoleViewModel?> RemoveByUserIdAsync(int userId, CancellationToken cancel = default);
    }
}
