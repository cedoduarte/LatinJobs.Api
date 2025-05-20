using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.DTOs
{
    public class CreateJobDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(256)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(256)]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Company is required")]
        [StringLength(256)]
        public string? Company { get; set; }

        [Required(ErrorMessage = "Employment type is required")]
        [StringLength(256)]
        public string? EmploymentType { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [StringLength(256)]
        public string? Salary { get; set; }

        [Required(ErrorMessage = "Posted date is required")]
        public DateTime? PostedDate { get; set; }

        [Required(ErrorMessage = "Company URL is required")]
        [StringLength(256)]
        public string? CompanyUrl { get; set; }

        [Required(ErrorMessage = "Company logo is required")]
        [StringLength(256)]
        public string? CompanyLogo { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
    }
}
