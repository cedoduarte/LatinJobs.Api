using LatinJobs.Api.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.Entities
{
    public class User : ITrackedEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required]
        [StringLength(256)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(256)]
        public string? LastName { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; } = null;
        public DateTime? Deleted { get; set; } = null;
    }
}
