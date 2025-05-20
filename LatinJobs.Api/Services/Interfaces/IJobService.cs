using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;
using LatinJobs.Api.ViewModels;

namespace LatinJobs.Api.Services.Interfaces
{
    public interface IJobService
    {
        Task<JobViewModel> CreateAsync(CreateJobDto createJobDto, CancellationToken cancel = default);
        Task<IEnumerable<JobViewModel>> FindAllAsync(CancellationToken cancel = default);
        Task<JobViewModel?> FindOneAsync(int id, CancellationToken cancel = default);
        Task<JobViewModel?> UpdateAsync(UpdateJobDto updateJobDto, CancellationToken cancel = default);
        Task<JobViewModel?> SoftDeleteAsync(int id, CancellationToken cancel = default);
        Task<JobViewModel?> RemoveAsync(int id, CancellationToken cancel = default);
    }
}
