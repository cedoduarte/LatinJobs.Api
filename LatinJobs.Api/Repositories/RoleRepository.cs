using LatinJobs.Api.Entities;
using LatinJobs.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LatinJobs.Api.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Role> CreateAsync(Role role, CancellationToken cancel)
        {
            var newRole = await _context.AddAsync(role, cancel);
            await _context.SaveChangesAsync(cancel);
            return newRole.Entity;
        }

        public async Task<IEnumerable<Role>> FindAllAsync(CancellationToken cancel)
        {
            return await _context.Roles
                .AsNoTracking()
                .ToListAsync(cancel);
        }

        public async Task<Role?> FindOneAsync(int id, CancellationToken cancel)
        {
            return await _context.Roles
                .Where(r => r.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancel);
        }

        public async Task<Role?> FindOneAsync(string name, CancellationToken cancel)
        {
            return await _context.Roles
                .Where(r => string.Equals(r.Name, name))
                .AsNoTracking()
                .FirstOrDefaultAsync(cancel);
        }

        public async Task<Role?> UpdateAsync(Role role, CancellationToken cancel)
        {
            var existingRole = await _context.Roles
                .Where(r => r.Id == role.Id)
                .FirstOrDefaultAsync(cancel);

            if (existingRole is null)
            {
                return null;
            }

            existingRole.Name = role.Name;
            existingRole.Updated = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancel);
            return existingRole;
        }

        public async Task<Role?> SoftDelete(int id, CancellationToken cancel)
        {
            var existingRole = await _context.Roles
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync(cancel);

            if (existingRole is null)
            {
                return null;
            }

            existingRole.IsDeleted = true;
            existingRole.Deleted = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancel);
            return existingRole;
        }

        public async Task<Role?> RemoveAsync(int id, CancellationToken cancel)
        {
            var foundRole = await _context.Roles
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync(cancel);

            if (foundRole is null)
            {
                return null;
            }

            _context.Roles.Remove(foundRole);
            await _context.SaveChangesAsync(cancel);
            return foundRole;
        }
    }
}
