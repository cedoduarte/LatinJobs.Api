using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;
using LatinJobs.Api.Exceptions;
using LatinJobs.Api.Repositories.Interfaces;
using LatinJobs.Api.Services.Interfaces;
using LatinJobs.Api.ViewModels;
using Mapster;

namespace LatinJobs.Api.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PermissionViewModel> CreateAsync(CreatePermissionDto createPermissionDto, CancellationToken cancel)
        {
            var createdPermission = await _unitOfWork.PermissionRepository.CreateAsync(new Permission
            {
                Name = createPermissionDto.Name!.Trim()
            }, cancel);
            return createdPermission.Adapt<PermissionViewModel>();
        }

        public async Task<IEnumerable<PermissionViewModel>> FindAllAsync(CancellationToken cancel)
        {
            var permissions = await _unitOfWork.PermissionRepository.FindAllAsync(cancel);
            return permissions.Adapt<IEnumerable<PermissionViewModel>>();
        }

        public async Task<PermissionViewModel?> FindOneAsync(int id, CancellationToken cancel)
        {
            var permission = await _unitOfWork.PermissionRepository.FindOneAsync(id, cancel);
            if (permission is null)
            {
                throw new NotFoundException($"Permission Not Found, ID = {id}");
            }
            return permission.Adapt<PermissionViewModel>();
        }

        public async Task<PermissionViewModel?> UpdateAsync(UpdatePermissionDto udatePermissionDto, CancellationToken cancel)
        {
            var existingPermission = new Permission
            {
                Id = udatePermissionDto.Id ?? 0,
                Name = udatePermissionDto.Name!.Trim()
            };

            var updatedPermission = await _unitOfWork.PermissionRepository.UpdateAsync(existingPermission, cancel);
            if (updatedPermission is null)
            {
                throw new NotFoundException($"Permission Not Found, ID = {udatePermissionDto.Id}");
            }
            return updatedPermission.Adapt<PermissionViewModel>();
        }

        public async Task<PermissionViewModel?> SoftDeleteAsync(int id, CancellationToken cancel)
        {
            var softDeletedPermission = await _unitOfWork.PermissionRepository.SoftDelete(id, cancel);
            if (softDeletedPermission is null)
            {
                throw new NotFoundException($"Permission Not Found, ID = {id}");
            }
            return softDeletedPermission.Adapt<PermissionViewModel>();
        }

        public async Task<PermissionViewModel?> RemoveAsync(int id, CancellationToken cancel)
        {
            var removedPermission = await _unitOfWork.PermissionRepository.RemoveAsync(id, cancel);
            if (removedPermission is null)
            {
                throw new NotFoundException($"Permission Not Found, ID = {id}");
            }
            return removedPermission.Adapt<PermissionViewModel>();
        }
    }
}
