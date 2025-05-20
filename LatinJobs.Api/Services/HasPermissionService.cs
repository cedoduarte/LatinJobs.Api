using LatinJobs.Api.Entities.Interfaces;
using LatinJobs.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LatinJobs.Api.Services
{
    public class HasPermissionService : IHasPermissionService
    {
        private readonly IAppDbContext _context;

        public HasPermissionService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasPermissionAsync(string userIdString, string requiredPermission, CancellationToken cancel)
        {
            if (!int.TryParse(userIdString, out int userId))
            {
                return false;
            }

            var hasPermission = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Join(_context.RolePermissions,
                      ur => ur.RoleId,
                      rp => rp.RoleId,
                      (ur, rp) => rp.PermissionId)
                .Join(_context.Permissions,
                      rpPermissionId => rpPermissionId,
                      p => p.Id,
                      (rpPermissionId, p) => p)
                .AnyAsync(p => p.Name == requiredPermission && !p.IsDeleted, cancel);

            return hasPermission;
        }
    }
}
