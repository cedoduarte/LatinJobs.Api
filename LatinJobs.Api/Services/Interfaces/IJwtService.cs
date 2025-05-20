namespace LatinJobs.Api.Services.Interfaces
{
    public interface IJwtService
    {
        public string GetJwtToken(int userId, int expirationSeconds);
    }
}
