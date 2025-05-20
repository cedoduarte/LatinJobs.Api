using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;
using LatinJobs.Api.Exceptions;
using LatinJobs.Api.Repositories.Interfaces;
using LatinJobs.Api.Services.Interfaces;
using LatinJobs.Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LatinJobs.Api.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly AppDbContext _context;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RolePermissionService(AppDbContext context, IRolePermissionRepository rolePermissionRepository)
        {
            _context = context;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<RolePermissionViewModel> CreateAsync(CreateRolePermissionDto createRolePermissionDto, CancellationToken cancel)
        {
            var foundRole = await _context.Roles
                .Where(x => x.Id == createRolePermissionDto.RoleId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancel);

            if (foundRole is null)
            {
                throw new NotFoundException($"Role Not Found, ID = {createRolePermissionDto.RoleId}");
            }
            
            var foundPermission = await _context.Permissions
                .Where(x => x.Id == createRolePermissionDto.PermissionId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancel);

            if (foundPermission is null)
            {
                throw new NotFoundException($"Permission Not Found, ID = {createRolePermissionDto.PermissionId}");
            }

            var existingRolePermission = await _context.RolePermissions
                .Where(rp => rp.RoleId == createRolePermissionDto.RoleId
                       && rp.PermissionId == createRolePermissionDto.PermissionId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancel);

            if (existingRolePermission is not null)
            {
                throw new AlreadyExistsException("The Role-Permission already exists");
            }

            var newRolePermission = new RolePermission
            {
                RoleId = createRolePermissionDto.RoleId ?? 0,
                PermissionId = createRolePermissionDto.PermissionId ?? 0
            };

            var createdRolePermission = await _rolePermissionRepository.CreateAsync(newRolePermission, cancel);

            var fullRolePermission = await _context.RolePermissions
                .Where(x => x.Id == createdRolePermission!.Id)
                .Include(x => x.Role)
                .Include(x => x.Permission)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancel);

            return new RolePermissionViewModel
            {
                Id = fullRolePermission!.Id,
                RoleId = fullRolePermission.RoleId,
                PermissionId = fullRolePermission.PermissionId,
                RoleName = fullRolePermission.Role!.Name,
                PermissionName = fullRolePermission.Permission!.Name
            };
        }

        public async Task<IEnumerable<RolePermissionViewModel>> FindAllAsync(CancellationToken cancel)
        {
            return await _context.RolePermissions
                .Include(x => x.Role)
                .Include(x => x.Permission)
                .AsNoTracking()
                .Select(x => new RolePermissionViewModel
                {
                    Id = x.Id,
                    RoleId = x.RoleId,
                    PermissionId = x.PermissionId,
                    RoleName = x.Role!.Name,
                    PermissionName = x.Permission!.Name
                })
                .ToListAsync(cancel);
        }

        public async Task<IEnumerable<PermissionViewModel>> GetPermissions(int roleId, CancellationToken cancel)
        {
            return await _context.RolePermissions
                .Where(x => x.RoleId == roleId)
                .AsNoTracking()
                .Select(x => x.Permission)
                .Select(x => new PermissionViewModel
                {
                    Id = x!.Id,
                    Name = x.Name
                })
                .ToListAsync(cancel);
        }

        public async Task<RolePermissionViewModel> RemoveAsync(int roleId, int permissionId, CancellationToken cancel)
        {
            var foundRolePermission = await _context.RolePermissions
                .Where(x => x.RoleId == roleId && x.PermissionId == permissionId)
                .Include(x => x.Role)
                .Include(x => x.Permission)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancel);

            if (foundRolePermission is null)
            {
                throw new NotFoundException($"Role-Permission Not Found");
            }

            await _rolePermissionRepository.RemoveAsync(foundRolePermission.Id, cancel);

            return new RolePermissionViewModel
            {
                Id = foundRolePermission.Id,
                RoleId = foundRolePermission.RoleId,
                PermissionId = foundRolePermission.PermissionId,
                RoleName = foundRolePermission.Role!.Name,
                PermissionName = foundRolePermission.Permission!.Name
            };
        }
    }
}
