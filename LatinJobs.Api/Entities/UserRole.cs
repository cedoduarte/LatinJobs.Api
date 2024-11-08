using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.Entities
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsertId { get; set; }

        [Required]
        public int RoleId { get; set; }

        public virtual User? User { get; set; }
        public virtual Role? Role { get; set; }
    }
}
