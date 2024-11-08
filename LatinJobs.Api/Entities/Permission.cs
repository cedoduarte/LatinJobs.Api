using LatinJobs.Api.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.Entities
{
    public class Permission : ITrackedEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; } = null;
        public DateTime? Deleted { get; set; } = null;
    }
}
