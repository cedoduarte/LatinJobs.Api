using LatinJobs.Api.DTOs;
using LatinJobs.Api.ViewModels;

namespace LatinJobs.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> CreateAsync(CreateUserDto createUserDto, CancellationToken cancel = default);
    }
}
