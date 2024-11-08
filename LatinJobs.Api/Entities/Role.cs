using LatinJobs.Api.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.Entities
{
    public class Role : ITrackedEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; } = null;
        public DateTime? Deleted { get; set; } = null;
    }
}
