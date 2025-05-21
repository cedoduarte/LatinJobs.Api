namespace LatinJobs.Api.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IPermissionRepository PermissionRepository { get; }
        IRolePermissionRepository RolePermissionRepository { get; }
        IUserAuthenticationRepository UserAuthenticationRepository { get; }
        IJobRepository JobRepository { get; }
    }
}
