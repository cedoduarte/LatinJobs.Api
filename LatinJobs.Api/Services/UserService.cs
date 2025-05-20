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

        public async Task<IEnumerable<UserViewModel>> FindAllAsync(CancellationToken cancel)
        {
            var users = await _userRepository.FindAllAsync(cancel);
            return users.Select(user => new UserViewModel 
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            });
        }

        public async Task<UserViewModel?> FindOneAsync(int id, CancellationToken cancel)
        {
            var user = await _userRepository.FindOneAsync(id, cancel);
            if (user is null)
            {
                throw new NotFoundException($"User Not Found, ID = {id}");
            }

            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public async Task<UserViewModel?> FindOneByEmailAsync(string email, CancellationToken cancel)
        {
            var user = await _userRepository.FindOneByEmail(email, cancel);
            if (user is null)
            {
                throw new NotFoundException($"User Not Found, Email = {email}");
            }

            return new UserViewModel 
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public async Task<UserViewModel?> UpdateAsync(UpdateUserDto updateUserDto, CancellationToken cancel)
        {
            var existingUser = new User
            {
                Id = updateUserDto.Id ?? 0,
                FirstName = updateUserDto.FirstName!.Trim(),
                LastName = updateUserDto.LastName!.Trim(),
                Email = updateUserDto.Email!.Trim(),
                PasswordHash = Utils.GetSha256Hash(updateUserDto.Password!.Trim())
            };

            var updatedUser = await _userRepository.UpdateAsync(existingUser, cancel);
            if (updatedUser is null)
            {
                throw new NotFoundException($"User Not Found, ID = {updateUserDto.Id}");
            }

            return new UserViewModel
            {
                Id = updatedUser.Id,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Email = updatedUser.Email
            };
        }

        public async Task<UserViewModel?> SoftDeleteAsync(int id, CancellationToken cancel)
        {
            var softDeletedUser = await _userRepository.SoftDelete(id, cancel);
            if (softDeletedUser is null)
            {
                throw new NotFoundException($"User Not Found, ID = {id}");
            }

            return new UserViewModel
            {
                Id = softDeletedUser.Id,
                FirstName = softDeletedUser.FirstName,
                LastName = softDeletedUser.LastName,
                Email = softDeletedUser.Email
            };
        }

        public async Task<UserViewModel?> RemoveAsync(int id, CancellationToken cancel)
        {
            var removedUser = await _userRepository.RemoveAsync(id, cancel);
            if (removedUser is null)
            {
                throw new NotFoundException($"User Not Found, ID = {id}");
            }

            return new UserViewModel
            {
                Id = removedUser.Id,
                FirstName = removedUser.FirstName,
                LastName = removedUser.LastName,
                Email = removedUser.Email
            };
        }
    }
}
