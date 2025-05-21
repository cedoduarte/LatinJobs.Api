using LatinJobs.Api.DTOs;
using LatinJobs.Api.Entities;
using LatinJobs.Api.Exceptions;
using LatinJobs.Api.Pagination;
using LatinJobs.Api.Repositories.Interfaces;
using LatinJobs.Api.Services.Interfaces;
using LatinJobs.Api.ViewModels;
using Mapster;

namespace LatinJobs.Api.Services
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<JobViewModel> CreateAsync(CreateJobDto createJobDto, CancellationToken cancel)
        {
            var createdJob = await _unitOfWork.JobRepository.CreateAsync(new Job
            {
                Title = createJobDto.Title!.Trim(),
                Description = createJobDto.Description!.Trim(),
                Location = createJobDto.Location!.Trim(),
                Company = createJobDto.Company!.Trim(),
                EmploymentType = createJobDto.EmploymentType!.Trim(),
                Salary = createJobDto.Salary!.Trim(),
                PostedDate = createJobDto.PostedDate,
                CompanyUrl = createJobDto.CompanyUrl?.Trim(),
                CompanyLogo = createJobDto.CompanyLogo?.Trim(),
                UserId = createJobDto.UserId
            }, cancel);

            return createdJob.Adapt<JobViewModel>();
        }

        public async Task<PagedResult<JobViewModel>> FindAllAsync(PaginationParametersDto paginationParametersDto, CancellationToken cancel)
        {
            return await _unitOfWork.JobRepository.FindAllAsync(paginationParametersDto, cancel);
        }

        public async Task<JobViewModel?> FindOneAsync(int id, CancellationToken cancel)
        {
            var job = await _unitOfWork.JobRepository.FindOneAsync(id, cancel);
            if (job is null)
            {
                throw new NotFoundException($"Job Not Found, ID = {id}");
            }
            return job.Adapt<JobViewModel>();
        }

        public async Task<JobViewModel?> UpdateAsync(UpdateJobDto updateJobDto, CancellationToken cancel)
        {
            var existingJob = new Job 
            {
                Id = updateJobDto.Id ?? 0,
                Title = updateJobDto.Title!.Trim(),
                Description = updateJobDto.Description!.Trim(),
                Location = updateJobDto.Location!.Trim(),
                Company = updateJobDto.Company!.Trim(),
                EmploymentType = updateJobDto.EmploymentType!.Trim(),
                Salary = updateJobDto.Salary!.Trim(),
                PostedDate = updateJobDto.PostedDate,
                CompanyUrl = updateJobDto.CompanyUrl!.Trim(),
                CompanyLogo = updateJobDto.CompanyLogo!.Trim(),
                UserId = updateJobDto.UserId
            };

            var updatedJob = await _unitOfWork.JobRepository.UpdateAsync(existingJob, cancel);
            if (updatedJob is null)
            {
                throw new NotFoundException($"Job Not Found, ID = {updateJobDto.Id}");
            }

            return updatedJob.Adapt<JobViewModel>();
        }

        public async Task<JobViewModel?> SoftDeleteAsync(int id, CancellationToken cancel)
        {
            var softDeletedJob = await _unitOfWork.JobRepository.SoftDelete(id, cancel);
            if (softDeletedJob is null)
            {
                throw new NotFoundException($"Job Not Found, ID = {id}");
            }
            return softDeletedJob.Adapt<JobViewModel>();
        }

        public async Task<JobViewModel?> RemoveAsync(int id, CancellationToken cancel)
        {
            var removedJob = await _unitOfWork.JobRepository.RemoveAsync(id, cancel);
            if (removedJob is null)
            {
                throw new NotFoundException($"Job Not Found, ID = {id}");
            }
            return removedJob.Adapt<JobViewModel>();
        }
    }
}
