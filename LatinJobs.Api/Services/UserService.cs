using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;
using LatinJobs.Api.Exceptions;
using LatinJobs.Api.Pagination;
using LatinJobs.Api.Repositories.Interfaces;
using LatinJobs.Api.Services.Interfaces;
using LatinJobs.Api.Shared;
using LatinJobs.Api.ViewModels;
using Mapster;

namespace LatinJobs.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
         }

        public async Task<UserViewModel> CreateAsync(CreateUserDto createUserDto, CancellationToken cancel)
        {
            var foundRole = await _unitOfWork.RoleRepository.FindOneAsync(createUserDto.RoleId ?? 0, cancel);
            if (foundRole is null)
            {
                throw new NotFoundException($"Role Not Found, ID = {createUserDto.RoleId}");
            }

            var createdUser = await _unitOfWork.UserRepository.CreateAsync(new User
            {
                Email = createUserDto.Email!.Trim(),
                PasswordHash = Utils.GetSha256Hash(createUserDto.Password!.Trim()),
                FirstName = createUserDto.FirstName!.Trim(),
                LastName = createUserDto.LastName!.Trim()
            }, cancel);

            await _unitOfWork.UserRoleRepository.CreateAsync(new UserRole()
            {
                UserId = createdUser!.Id,
                RoleId = foundRole!.Id
            });

            return createdUser.Adapt<UserViewModel>();
        }

        public async Task<PagedResult<UserViewModel>> FindAllAsync(PaginationParametersDto paginationParametersDto, CancellationToken cancel)
        {
            return await _unitOfWork.UserRepository.FindAllAsync(paginationParametersDto, cancel);
        }

        public async Task<UserViewModel?> FindOneAsync(int id, CancellationToken cancel)
        {
            var user = await _unitOfWork.UserRepository.FindOneAsync(id, cancel);
            if (user is null)
            {
                throw new NotFoundException($"User Not Found, ID = {id}");
            }
            return user.Adapt<UserViewModel>();
        }

        public async Task<UserViewModel?> FindOneByEmailAsync(string email, CancellationToken cancel)
        {
            var user = await _unitOfWork.UserRepository.FindOneByEmail(email, cancel);
            if (user is null)
            {
                throw new NotFoundException($"User Not Found, Email = {email}");
            }
            return user.Adapt<UserViewModel>();
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

            var updatedUser = await _unitOfWork.UserRepository.UpdateAsync(existingUser, cancel);
            if (updatedUser is null)
            {
                throw new NotFoundException($"User Not Found, ID = {updateUserDto.Id}");
            }
            return updatedUser.Adapt<UserViewModel>();
        }

        public async Task<UserViewModel?> SoftDeleteAsync(int id, CancellationToken cancel)
        {
            var softDeletedUser = await _unitOfWork.UserRepository.SoftDelete(id, cancel);
            if (softDeletedUser is null)
            {
                throw new NotFoundException($"User Not Found, ID = {id}");
            }
            return softDeletedUser.Adapt<UserViewModel>();
        }

        public async Task<UserViewModel?> RemoveAsync(int id, CancellationToken cancel)
        {
            var removedUser = await _unitOfWork.UserRepository.RemoveAsync(id, cancel);
            if (removedUser is null)
            {
                throw new NotFoundException($"User Not Found, ID = {id}");
            }
            return removedUser.Adapt<UserViewModel>();
        }
    }
}
