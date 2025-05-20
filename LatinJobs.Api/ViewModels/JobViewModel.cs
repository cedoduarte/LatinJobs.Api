using System.ComponentModel.DataAnnotations;

namespace LatinJobs.Api.ViewModels
{
    public class JobViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? Company { get; set; }
        public string? EmploymentType { get; set; }
        public string? Salary { get; set; }
        public DateTime? PostedDate { get; set; }
        public string? CompanyUrl { get; set; }
        public string? CompanyLogo { get; set; }
        public int UserId { get; set; }
    }
}
