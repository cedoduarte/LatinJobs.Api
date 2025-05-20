using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.DTOs
{
    public class CreateRolePermissionDto
    {
        [Required(ErrorMessage = "Role ID is required")]
        public int? RoleId { get; set; }

        [Required(ErrorMessage = "Permission ID is required")]
        public int? PermissionId { get; set; }
    }
}
