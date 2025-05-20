namespace LatinJobs.Api.Services.Interfaces
{
    public interface IHasPermissionService
    {
        Task<bool> HasPermissionAsync(string userIdString, string requiredPermission, CancellationToken cancel = default);
    }
}
