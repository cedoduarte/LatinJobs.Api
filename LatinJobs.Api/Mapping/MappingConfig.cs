using LatinJobs.Api.Entities;
using LatinJobs.Api.ViewModels;
using Mapster;

namespace LatinJobs.Api.Mapping
{
    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Job, JobViewModel>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Location, src => src.Location)
                .Map(dest => dest.Company, src => src.Company)
                .Map(dest => dest.EmploymentType, src => src.EmploymentType)
                .Map(dest => dest.Salary, src => src.Salary)
                .Map(dest => dest.PostedDate, src => src.PostedDate)
                .Map(dest => dest.CompanyUrl, src => src.CompanyUrl)
                .Map(dest => dest.CompanyLogo, src => src.CompanyLogo)
                .Map(dest => dest.UserId, src => src.UserId);

            TypeAdapterConfig<Permission, PermissionViewModel>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name);

            TypeAdapterConfig<RolePermission, RolePermissionViewModel>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.RoleId, src => src.RoleId)
                .Map(dest => dest.RoleName, src => src.Role!.Name)
                .Map(dest => dest.PermissionId, src => src.PermissionId)
                .Map(dest => dest.PermissionName, src => src.Permission!.Name);

            TypeAdapterConfig<Role, RoleViewModel>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name);

            TypeAdapterConfig<User, UserViewModel>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName);
        }
    }
}
