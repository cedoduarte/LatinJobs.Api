using LatinJobs.Api.Entities;

namespace LatinJobs.Api.Repositories.Interfaces
{
    public interface IJobRepository
    {
        Task<Job> CreateAsync(Job job, CancellationToken cancel = default);
        Task<IEnumerable<Job>> FindAllAsync(CancellationToken cancel = default);
        Task<Job?> FindOneAsync(int id, CancellationToken cancel = default);
        Task<Job?> UpdateAsync(Job job, CancellationToken cancel = default);
        Task<Job?> SoftDelete(int id, CancellationToken cancel = default);
        Task<Job?> RemoveAsync(int id, CancellationToken cancel = default);
    }
}
