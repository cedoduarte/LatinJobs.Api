using LatinJobs.Api.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.Entities
{
    public class Job : ITrackedEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        [StringLength(256)]
        public string? Location { get; set; }

        [Required]
        [StringLength(256)]
        public string? Company { get; set; }

        [Required]
        [StringLength(256)]
        public string? EmploymentType { get; set; }

        [Required]
        [StringLength(256)]
        public string? Salary { get; set; }

        [Required]
        public DateTime? PostedDate { get; set; }

        [Required]
        [StringLength(256)]
        public string? CompanyUrl { get; set; }

        [Required]
        [StringLength(256)]
        public string? CompanyLogo { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; } = null;
        public DateTime? Deleted { get; set; } = null;
    }
}
