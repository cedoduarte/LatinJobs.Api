using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.DTOs
{
    public class CreatePermissionDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string? Name { get; set; }
    }
}
