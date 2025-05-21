using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;
using LatinJobs.Api.Exceptions;
using LatinJobs.Api.Repositories.Interfaces;
using LatinJobs.Api.Services.Interfaces;
using LatinJobs.Api.ViewModels;
using Microsoft.AspNetCore.Server.Kestrel.Transport.NamedPipes;

namespace LatinJobs.Api.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserRoleViewModel> CreateAsync(CreateUserRoleDto createUserRoleDto, CancellationToken cancel)
        {
            var existingUserRole = await _unitOfWork.UserRoleRepository.FindOneByUserIdAsync(createUserRoleDto.UserId ?? 0, cancel);
            if (existingUserRole is not null)
            {
                throw new AlreadyExistsException("The specified User already has a role");
            }

            var foundUser = await _unitOfWork.UserRepository.FindOneAsync(createUserRoleDto.UserId ?? 0, cancel);
            if (foundUser is null)
            {
                throw new NotFoundException($"User Not Found, ID = {createUserRoleDto.UserId}");
            }

            var foundRole = await _unitOfWork.RoleRepository.FindOneAsync(createUserRoleDto.RoleId ?? 0, cancel);
            if (foundRole is null)
            {
                throw new NotFoundException($"Role Not Found, ID = {createUserRoleDto.RoleId}");
            }

            var createdUserRole = await _unitOfWork.UserRoleRepository.CreateAsync(new UserRole
            {
                UserId = foundUser.Id,
                RoleId = foundRole.Id
            }, cancel);

            return new UserRoleViewModel
            {
                User = new UserViewModel
                {
                    Id = createdUserRole.UserId,
                    FirstName = foundUser.FirstName,
                    LastName = foundUser.LastName,
                    Email = foundUser.Email
                },
                Role = new RoleViewModel
                {
                    Id = createdUserRole.RoleId,
                    Name = foundRole.Name
                }
            };
        }

        public async Task<IEnumerable<UserRoleViewModel>> FindAllAsync(CancellationToken cancel)
        {
            var userRoles = await _unitOfWork.UserRoleRepository.FindAllAsync(cancel);
            return userRoles.Select(userRole => new UserRoleViewModel
            {
                User = new UserViewModel
                {
                    Id = userRole.UserId,
                    FirstName = userRole.User!.FirstName,
                    LastName = userRole.User!.LastName,
                    Email = userRole.User!.Email
                },
                Role = new RoleViewModel
                {
                    Id = userRole.RoleId,
                    Name = userRole.Role!.Name
                }
            });
        }

        public async Task<UserRoleViewModel?> FindOneByUserIdAsync(int userId, CancellationToken cancel)
        {
            var userRole = await _unitOfWork.UserRoleRepository.FindOneByUserIdAsync(userId, cancel);
            if (userRole is null)
            {
                throw new NotFoundException("User-Role Not Found");
            }

            return new UserRoleViewModel
            {
                User = new UserViewModel
                {
                    Id = userRole.UserId,
                    FirstName = userRole.User!.FirstName,
                    LastName = userRole.User!.LastName,
                    Email = userRole.User!.Email
                },
                Role = new RoleViewModel
                {
                    Id = userRole.RoleId,
                    Name = userRole.Role!.Name
                }
            };
        }

        public async Task<UserRoleViewModel?> UpdateAsync(UpdateUserRoleDto updateUserRoleDto, CancellationToken cancel)
        {
            var existingUser = await _unitOfWork.UserRepository.FindOneAsync(updateUserRoleDto.UserId ?? 0, cancel);
            if (existingUser is null)
            {
                throw new NotFoundException($"User Not Found, ID = {updateUserRoleDto.UserId ?? 0}");
            }

            var existingRole = await _unitOfWork.RoleRepository.FindOneAsync(updateUserRoleDto.RoleId ?? 0, cancel);
            if (existingRole is null)
            {
                throw new NotFoundException($"Role Not Found, ID = {updateUserRoleDto.RoleId ?? 0}");
            }

            var existingUserRole = new UserRole
            {
                Id = updateUserRoleDto.Id ?? 0,
                UserId = updateUserRoleDto.UserId ?? 0,
                RoleId = updateUserRoleDto.RoleId ?? 0
            };

            var updatedUserRole = await _unitOfWork.UserRoleRepository.UpdateAsync(existingUserRole, cancel);
            if (updatedUserRole is null)
            {
                throw new NotFoundException("User-Role Not Found");
            }

            return new UserRoleViewModel
            {
                User = new UserViewModel
                {
                    Id = updatedUserRole.UserId,
                    FirstName = updatedUserRole.User!.FirstName,
                    LastName = updatedUserRole.User!.LastName,
                    Email = updatedUserRole.User!.Email
                },
                Role = new RoleViewModel
                {
                    Id = updatedUserRole.RoleId,
                    Name = updatedUserRole.Role!.Name
                }
            };
        }

        public async Task<UserRoleViewModel?> RemoveByUserIdAsync(int userId, CancellationToken cancel)
        {
            var removedUserRole = await _unitOfWork.UserRoleRepository.RemoveByUserIdAsync(userId, cancel);
            if (removedUserRole is null)
            {
                throw new NotFoundException("User-Role Not Found");
            }

            return new UserRoleViewModel
            {
                User = new UserViewModel
                {
                    Id = removedUserRole.UserId,
                    FirstName = removedUserRole.User!.FirstName,
                    LastName = removedUserRole.User!.LastName,
                    Email = removedUserRole.User!.Email
                },
                Role = new RoleViewModel
                {
                    Id = removedUserRole.RoleId,
                    Name = removedUserRole.Role!.Name
                }
            };
        }
    }
}
