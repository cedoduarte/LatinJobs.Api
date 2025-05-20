using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.DTOs
{
    public class UpdateJobDto : CreateJobDto
    {
        [Required]
        public int? Id { get; set; }
    }
}
