using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;
using LatinJobs.Api.Exceptions;
using LatinJobs.Api.Repositories.Interfaces;
using LatinJobs.Api.Services.Interfaces;
using LatinJobs.Api.ViewModels;
using Mapster;

namespace LatinJobs.Api.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<RoleViewModel> CreateAsync(CreateRoleDto createRoleDto, CancellationToken cancel)
        {
            var createdRole = await _roleRepository.CreateAsync(new Role
            {
                Name = createRoleDto.Name!.Trim()
            }, cancel);
            return createdRole.Adapt<RoleViewModel>();
        }

        public async Task<IEnumerable<RoleViewModel>> FindAllAsync(CancellationToken cancel)
        {
            var roles = await _roleRepository.FindAllAsync(cancel);
            return roles.Adapt<IEnumerable<RoleViewModel>>();
        }

        public async Task<RoleViewModel?> FindOneAsync(int id, CancellationToken cancel)
        {
            var role = await _roleRepository.FindOneAsync(id, cancel);
            if (role is null)
            {
                throw new NotFoundException($"Role Not Found, ID = {id}");
            }
            return role.Adapt<RoleViewModel>();
        }

        public async Task<RoleViewModel?> FindOneAsync(string name, CancellationToken cancel)
        {
            var role = await _roleRepository.FindOneAsync(name, cancel);
            if (role is null)
            {
                throw new NotFoundException($"Role Not Found, Name = {name}");
            }
            return role.Adapt<RoleViewModel>();
        }

        public async Task<RoleViewModel?> UpdateAsync(UpdateRoleDto updateRoleDto, CancellationToken cancel)
        {
            var existingRole = new Role 
            {
                Id = updateRoleDto.Id ?? 0,
                Name = updateRoleDto.Name!.Trim()
            };

            var updatedRole = await _roleRepository.UpdateAsync(existingRole, cancel);
            if (updatedRole is null)
            {
                throw new NotFoundException($"Role Not Found, ID = {existingRole.Id}");
            }

            return updatedRole.Adapt<RoleViewModel>();
        }

        public async Task<RoleViewModel?> SoftDeleteAsync(int id, CancellationToken cancel)
        {
            var softDeletedRole = await _roleRepository.SoftDelete(id, cancel);
            if (softDeletedRole is null)
            {
                throw new NotFoundException($"Role Not Found, ID = {id}");
            }
            return softDeletedRole.Adapt<RoleViewModel>();
        }

        public async Task<RoleViewModel?> RemoveAsync(int id, CancellationToken cancel)
        {
            var removedRole = await _roleRepository.RemoveAsync(id, cancel);
            if (removedRole is null)
            {
                throw new NotFoundException($"Role Not Found, ID = {id}");
            }
            return removedRole.Adapt<RoleViewModel>();
        }
    }
}
