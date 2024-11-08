using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.Entities
{
    public class RolePermission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int PermissionId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual Permission? Permission { get; set; }
    }
}
