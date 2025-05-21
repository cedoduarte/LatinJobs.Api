using LatinJobs.Api.Repositories.Interfaces;

namespace LatinJobs.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public IPermissionRepository PermissionRepository { get; }
        public IRolePermissionRepository RolePermissionRepository { get; }
        public IUserAuthenticationRepository UserAuthenticationRepository { get; }
        public IJobRepository JobRepository { get; }

        public UnitOfWork(
            IUserRepository userRepository, 
            IRoleRepository roleRepository, 
            IUserRoleRepository userRoleRepository, 
            IPermissionRepository permissionRepository, 
            IRolePermissionRepository rolePermissionRepository, 
            IUserAuthenticationRepository userAuthenticationRepository, 
            IJobRepository jobRepository)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            UserRoleRepository = userRoleRepository;
            PermissionRepository = permissionRepository;
            RolePermissionRepository = rolePermissionRepository;
            UserAuthenticationRepository = userAuthenticationRepository;
            JobRepository = jobRepository;
        }
    }
}
