using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;
using LatinJobs.Api.Pagination;
using LatinJobs.Api.ViewModels;

namespace LatinJobs.Api.Repositories.Interfaces
{
    public interface IJobRepository
    {
        Task<Job> CreateAsync(Job job, CancellationToken cancel = default);
        Task<PagedResult<JobViewModel>> FindAllAsync(PaginationParametersDto paginationParametersDto, CancellationToken cancel = default);
        Task<Job?> FindOneAsync(int id, CancellationToken cancel = default);
        Task<Job?> UpdateAsync(Job job, CancellationToken cancel = default);
        Task<Job?> SoftDelete(int id, CancellationToken cancel = default);
        Task<Job?> RemoveAsync(int id, CancellationToken cancel = default);
    }
}
