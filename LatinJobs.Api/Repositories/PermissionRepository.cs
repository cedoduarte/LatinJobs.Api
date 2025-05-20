using LatinJobs.Api.Entities;
using LatinJobs.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LatinJobs.Api.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _context;

        public PermissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Permission> CreateAsync(Permission permission, CancellationToken cancel)
        {
            var newPermission = await _context.AddAsync(permission, cancel);
            await _context.SaveChangesAsync(cancel);
            return newPermission.Entity;
        }

        public async Task<IEnumerable<Permission>> FindAllAsync(CancellationToken cancel)
        {
            return await _context.Permissions
                .AsNoTracking()
                .ToListAsync(cancel);
        }

        public async Task<Permission?> FindOneAsync(int id, CancellationToken cancel)
        {
            return await _context.Permissions
                .Where(p => p.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancel);
        }

        public async Task<Permission?> UpdateAsync(Permission permission, CancellationToken cancel)
        {
            var existingPermission = await _context.Permissions
                .Where(p => p.Id == permission.Id)
                .FirstOrDefaultAsync(cancel);

            if (existingPermission is null)
            {
                return null;
            }

            existingPermission.Name = permission.Name;
            existingPermission.Updated = DateTime.UtcNow;
            
            await _context.SaveChangesAsync(cancel);
            return existingPermission;
        }

        public async Task<Permission?> SoftDelete(int id, CancellationToken cancel)
        {
            var existingPermission = await _context.Permissions
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync(cancel);
        
            if (existingPermission is null)
            {
                return null;
            }

            existingPermission.IsDeleted = true;
            existingPermission.Deleted = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancel);
            return existingPermission;
        }

        public async Task<Permission?> RemoveAsync(int id, CancellationToken cancel)
        {
            var foundPermission = await _context.Permissions
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync(cancel);
        
            if (foundPermission is null)
            {
                return null;
            }

            _context.Permissions.Remove(foundPermission);
            await _context.SaveChangesAsync(cancel);
            return foundPermission;
        }
    }
}
