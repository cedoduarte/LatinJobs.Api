using LatinJobs.Api.Entities;
using LatinJobs.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LatinJobs.Api.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _context;

        public UserRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserRole> CreateAsync(UserRole userRole, CancellationToken cancel)
        {
            var newUserRole = await _context.AddAsync(userRole, cancel);
            await _context.SaveChangesAsync(cancel);
            return newUserRole.Entity;
        }

        public async Task<IEnumerable<UserRole>> FindAllAsync(CancellationToken cancel)
        {
            return await _context.UserRoles
                .AsNoTracking()
                .ToListAsync(cancel);
        }

        public async Task<UserRole?> FindOneByUserIdAsync(int userId, CancellationToken cancel)
        {
            var foundUserRole = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancel);

            return foundUserRole;
        }

        public async Task<UserRole?> UpdateAsync(UserRole userRole, CancellationToken cancel)
        {
            var existingUserRole = await _context.UserRoles
                .Where(ur => ur.Id == userRole.Id)
                .FirstOrDefaultAsync(cancel);

            if (existingUserRole is null)
            {
                return null;
            }

            existingUserRole.UserId = userRole.UserId;
            existingUserRole.RoleId = userRole.RoleId;

            await _context.SaveChangesAsync(cancel);
            return existingUserRole;
        }

        public async Task<UserRole?> RemoveByUserIdAsync(int userId, CancellationToken cancel)
        {
            var foundUserRole = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .FirstOrDefaultAsync(cancel);
            
            if (foundUserRole is null)
            {
                return null;
            }
            
            _context.UserRoles.Remove(foundUserRole);
            await _context.SaveChangesAsync(cancel);
            return foundUserRole;
        }
    }
}
