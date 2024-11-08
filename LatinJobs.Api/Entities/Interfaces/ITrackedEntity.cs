namespace LatinJobs.Api.Entities.Interfaces
{
    public interface ITrackedEntity
    {
        bool IsDeleted { get; set; }
        DateTime? Created { get; set; }
        DateTime? Updated { get; set; }
        DateTime? Deleted { get; set; }
    }
}
