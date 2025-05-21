using Microsoft.EntityFrameworkCore;

namespace LatinJobs.Api.Entities.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<UserAuthentication> UserAuthentications { get; }
        DbSet<Permission> Permissions { get; }
        DbSet<Role> Roles { get; }
        DbSet<RolePermission> RolePermissions { get; }
        DbSet<User> Users { get; }
        DbSet<UserRole> UserRoles { get; }
        DbSet<Job> Jobs { get; }
    }
}
