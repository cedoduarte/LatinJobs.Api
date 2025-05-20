namespace LatinJobs.Api.DTOs
{
    public class UserAuthenticatedDto
    {
        public int AuthenticationId { get; set; }
        public int UserId { get; set; }
        public DateTime? Date { get; set; }
    }
}
