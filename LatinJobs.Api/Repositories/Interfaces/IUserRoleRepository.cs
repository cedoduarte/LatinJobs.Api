﻿using LatinJobs.Api.Entities;

namespace LatinJobs.Api.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<UserRole> CreateAsync(UserRole userRole, CancellationToken cancel = default);
        Task<IEnumerable<UserRole>> FindAllAsync(CancellationToken cancel = default);
        Task<UserRole?> FindOneByUserIdAsync(int userId, CancellationToken cancel = default);
        Task<UserRole?> UpdateAsync(UserRole userRole, CancellationToken cancel = default);
        Task<UserRole?> RemoveByUserIdAsync(int userId, CancellationToken cancel = default);
    }
}
