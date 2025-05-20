namespace LatinJobs.Api.ViewModels
{
    public class UserAuthenticationViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Token { get; set; }
        public DateTime? Date { get; set; }
    }
}
