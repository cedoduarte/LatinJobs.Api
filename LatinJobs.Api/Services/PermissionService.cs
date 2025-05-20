using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;
using LatinJobs.Api.Exceptions;
using LatinJobs.Api.Repositories.Interfaces;
using LatinJobs.Api.Services.Interfaces;
using LatinJobs.Api.ViewModels;

namespace LatinJobs.Api.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<PermissionViewModel> CreateAsync(CreatePermissionDto createPermissionDto, CancellationToken cancel)
        {
            var createdPermission = await _permissionRepository.CreateAsync(new Entities.Permission
            {
                Name = createPermissionDto.Name!.Trim()
            }, cancel);

            return new PermissionViewModel
            {
                Id = createdPermission.Id,
                Name = createdPermission.Name
            };
        }

        public async Task<IEnumerable<PermissionViewModel>> FindAllAsync(CancellationToken cancel)
        {
            var permissions = await _permissionRepository.FindAllAsync(cancel);
            return permissions.Select(permission => new PermissionViewModel 
            {
                Id = permission.Id,
                Name = permission.Name
            });
        }

        public async Task<PermissionViewModel?> FindOneAsync(int id, CancellationToken cancel)
        {
            var permission = await _permissionRepository.FindOneAsync(id, cancel);
            if (permission is null)
            {
                throw new NotFoundException($"Permission Not Found, ID = {id}");
            }

            return new PermissionViewModel
            {
                Id = permission.Id,
                Name = permission.Name
            };
        }

        public async Task<PermissionViewModel?> UpdateAsync(UpdatePermissionDto udatePermissionDto, CancellationToken cancel)
        {
            var existingPermission = new Permission
            {
                Id = udatePermissionDto.Id ?? 0,
                Name = udatePermissionDto.Name!.Trim()
            };

            var updatedPermission = await _permissionRepository.UpdateAsync(existingPermission, cancel);
            if (updatedPermission is null)
            {
                throw new NotFoundException($"Permission Not Found, ID = {udatePermissionDto.Id}");
            }

            return new PermissionViewModel
            {
                Id = updatedPermission.Id,
                Name = updatedPermission.Name
            };
        }

        public async Task<PermissionViewModel?> SoftDelete(int id, CancellationToken cancel)
        {
            var softDeletedPermission = await _permissionRepository.SoftDelete(id, cancel);
            if (softDeletedPermission is null)
            {
                throw new NotFoundException($"Permission Not Found, ID = {id}");
            }

            return new PermissionViewModel
            {
                Id = softDeletedPermission.Id,
                Name = softDeletedPermission.Name
            };
        }

        public async Task<PermissionViewModel?> RemoveAsync(int id, CancellationToken cancel)
        {
            var removedPermission = await _permissionRepository.RemoveAsync(id, cancel);
            if (removedPermission is null)
            {
                throw new NotFoundException($"Permission Not Found, ID = {id}");
            }

            return new PermissionViewModel
            {
                Id = removedPermission.Id,
                Name = removedPermission.Name
            };
        }
    }
}
