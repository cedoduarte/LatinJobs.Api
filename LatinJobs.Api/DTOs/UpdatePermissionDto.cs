using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.DTOs
{
    public class UpdatePermissionDto : CreatePermissionDto
    {
        [Required]
        public int? Id { get; set; }
    }
}
