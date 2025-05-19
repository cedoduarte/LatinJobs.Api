using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;
using LatinJobs.Api.Exceptions;
using LatinJobs.Api.Repositories.Interfaces;
using LatinJobs.Api.Services.Interfaces;
using LatinJobs.Api.Shared;
using LatinJobs.Api.ViewModels;

namespace LatinJobs.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        public UserService(IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
         }

        public async Task<UserViewModel> CreateAsync(CreateUserDto createUserDto, CancellationToken cancel)
        {
            var foundRole = await _roleRepository.FindOneAsync(createUserDto.RoleId ?? 0, cancel);
            if (foundRole is null)
            {
                throw new NotFoundException($"Role Not Found, ID = {createUserDto.RoleId}");
            }

            var createdUser = await _userRepository.CreateAsync(new User
            {
                Email = createUserDto.Email!.Trim(),
                PasswordHash = Utils.GetSha256Hash(createUserDto.Password!.Trim()),
                FirstName = createUserDto.FirstName!.Trim(),
                LastName = createUserDto.LastName!.Trim()
            }, cancel);

            await _userRoleRepository.CreateAsync(new UserRole()
            {
                UserId = createdUser!.Id,
                RoleId = foundRole!.Id
            });

            return new UserViewModel
            {
                Id = createdUser.Id,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Email = createdUser.Email
            };
        }
    }
}
