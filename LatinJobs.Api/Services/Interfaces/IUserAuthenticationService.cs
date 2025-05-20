using LatinJobs.Api.DTOs;
using LatinJobs.Api.ViewModels;

namespace LatinJobs.Api.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<UserAuthenticationViewModel> CreateAsync(CreateUserAuthenticationDto createAuthenticationDto, CancellationToken cancel = default);
        Task<IEnumerable<UserAuthenticatedDto>> FindAllAsync(CancellationToken cancel = default);
        Task<IEnumerable<UserAuthenticatedDto>> FindByUserIdAsync(int userId, CancellationToken cancel = default);
        Task<UserAuthenticatedDto?> RemoveAsync(int id, CancellationToken cancel = default);
    }
}
