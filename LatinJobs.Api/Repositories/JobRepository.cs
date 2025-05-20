using LatinJobs.Api.Entities;
using LatinJobs.Api.Entities.Interfaces;
using LatinJobs.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LatinJobs.Api.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly AppDbContext _context;

        public JobRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Job> CreateAsync(Job job, CancellationToken cancel)
        {
            var newJob = await _context.Jobs.AddAsync(job, cancel);
            await _context.SaveChangesAsync(cancel);
            return newJob.Entity;
        }

        public async Task<IEnumerable<Job>> FindAllAsync(CancellationToken cancel)
        {
            return await _context.Jobs
                .AsNoTracking()
                .ToListAsync(cancel);
        }

        public async Task<Job?> FindOneAsync(int id, CancellationToken cancel)
        {
            return await _context.Jobs
                .Where(j => j.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancel);
        }

        public async Task<Job?> UpdateAsync(Job job, CancellationToken cancel)
        {
            var existingJob = await _context.Jobs
                .Where(j => j.Id == job.Id)
                .FirstOrDefaultAsync(cancel);

            if (existingJob is null)
            {
                return null;
            }

            existingJob.Title = job.Title;
            existingJob.Description = job.Description;
            existingJob.Location = job.Location;
            existingJob.Company = job.Company;
            existingJob.EmploymentType = job.EmploymentType;
            existingJob.Salary = job.Salary;
            existingJob.PostedDate = job.PostedDate;
            existingJob.CompanyUrl = job.CompanyUrl;
            existingJob.CompanyLogo = job.CompanyLogo;
            existingJob.Updated = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancel);
            return existingJob;
        }

        public async Task<Job?> SoftDelete(int id, CancellationToken cancel)
        {
            var existingJob = await _context.Jobs
                .Where(j => j.Id == id)
                .FirstOrDefaultAsync(cancel);

            if (existingJob is null)
            {
                return null;
            }

            existingJob.IsDeleted = true;
            existingJob.Deleted = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancel);
            return existingJob;
        }

        public async Task<Job?> RemoveAsync(int id, CancellationToken cancel)
        {
            var foundJob = await _context.Jobs
                .Where(j => j.Id == id)
                .FirstOrDefaultAsync(cancel);

            if (foundJob is null)
            {
                return null;
            }

            _context.Jobs.Remove(foundJob);
            await _context.SaveChangesAsync(cancel);
            return foundJob;
        }
    }
}
