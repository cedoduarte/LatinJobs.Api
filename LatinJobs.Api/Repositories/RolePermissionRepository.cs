using LatinJobs.Api.Entities;
using LatinJobs.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LatinJobs.Api.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly AppDbContext _context;

        public RolePermissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RolePermission> CreateAsync(RolePermission rolePermission, CancellationToken cancel)
        {
            var newRolePermission = await _context.AddAsync(rolePermission, cancel);
            await _context.SaveChangesAsync(cancel);
            return newRolePermission.Entity;
        }

        public async Task<IEnumerable<RolePermission>> FindAllAsync(CancellationToken cancel)
        {
            return await _context.RolePermissions
                .AsNoTracking()
                .ToListAsync(cancel);
        }

        public async Task<RolePermission?> FindOneAsync(int id, CancellationToken cancel)
        {
            return await _context.RolePermissions
                .Where(rp => rp.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancel);
        }

        public async Task<RolePermission?> UpdateAsync(RolePermission rolePermission, CancellationToken cancel)
        {
            var existingRolePermission = await _context.RolePermissions
                .Where(rp => rp.Id == rolePermission.Id)
                .FirstOrDefaultAsync(cancel);

            if (existingRolePermission is null)
            {
                return null;
            }

            existingRolePermission.RoleId = rolePermission.RoleId;
            existingRolePermission.PermissionId = rolePermission.PermissionId;

            await _context.SaveChangesAsync(cancel);
            return existingRolePermission;
        }
        
        public async Task<RolePermission?> RemoveAsync(int id, CancellationToken cancel)
        {
            var foundRolePermission = await _context.RolePermissions
                .Where(rp => rp.Id == id)
                .FirstOrDefaultAsync(cancel);

            if (foundRolePermission is null)
            {
                return null;
            }

            _context.RolePermissions.Remove(foundRolePermission);
            await _context.SaveChangesAsync(cancel);
            return foundRolePermission;
        }
    }
}
