using LatinJobs.Api.Entities;
using LatinJobs.Api.Shared;

namespace LatinJobs.Api.Models
{
    public static class DbSeeder
    {
        public static void DoSeeding(AppDbContext dbContext)
        {
            SeedUsers(dbContext);
            SeedRoles(dbContext);
            SeedPermissions(dbContext);
            SeedUserRoles(dbContext);
            SeedRolePermissions(dbContext);
        }

        private static void SeedRolePermissions(AppDbContext dbContext)
        {
            if (!dbContext.RolePermissions.Any())
            {
                var adminRole = dbContext.Roles.SingleOrDefault(x => string.Equals(x.Name, Constants.Roles.Admin));
                var userRole = dbContext.Roles.SingleOrDefault(x => string.Equals(x.Name, Constants.Roles.User));
                var guestRole = dbContext.Roles.SingleOrDefault(x => string.Equals(x.Name, Constants.Roles.Guest));

                var writePermission = dbContext.Permissions.SingleOrDefault(x => string.Equals(x.Name, Constants.Permissions.Write));
                var readPermission = dbContext.Permissions.SingleOrDefault(x => string.Equals(x.Name, Constants.Permissions.Read));
                var editPermission = dbContext.Permissions.SingleOrDefault(x => string.Equals(x.Name, Constants.Permissions.Edit));
                var deletePermission = dbContext.Permissions.SingleOrDefault(x => string.Equals(x.Name, Constants.Permissions.Delete));

                dbContext.RolePermissions.AddRange(new RolePermission[]
                {
                    new RolePermission()
                    {
                        RoleId = adminRole!.Id,
                        PermissionId = writePermission!.Id
                    },
                    new RolePermission()
                    {
                        RoleId = adminRole!.Id,
                        PermissionId = readPermission!.Id
                    },
                    new RolePermission()
                    {
                        RoleId = adminRole!.Id,
                        PermissionId = editPermission!.Id
                    },
                    new RolePermission()
                    {
                        RoleId = adminRole!.Id,
                        PermissionId = deletePermission!.Id
                    },
                    new RolePermission()
                    {
                        RoleId = userRole!.Id,
                        PermissionId = readPermission!.Id
                    },
                    new RolePermission()
                    {
                        RoleId = userRole!.Id,
                        PermissionId = editPermission!.Id
                    },
                    new RolePermission()
                    {
                        RoleId = guestRole!.Id,
                        PermissionId = readPermission!.Id
                    }
                });
                dbContext.SaveChanges();
            }
        }

        private static void SeedUserRoles(AppDbContext dbContext)
        {
            if (!dbContext.UserRoles.Any())
            {
                var userCarlos = dbContext.Users.SingleOrDefault(x => string.Equals(x.Email, "carlosduarte.1@hotmail.com"));
                var userAna = dbContext.Users.SingleOrDefault(x => string.Equals(x.Email, "ana@hotmail.com"));
                var userRosa = dbContext.Users.SingleOrDefault(x => string.Equals(x.Email, "rosa@hotmail.com"));
                var userLuis = dbContext.Users.SingleOrDefault(x => string.Equals(x.Email, "luis@hotmail.com"));
                var userAntonio = dbContext.Users.SingleOrDefault(x => string.Equals(x.Email, "antonio@hotmail.com"));

                var adminRole = dbContext.Roles.SingleOrDefault(x => string.Equals(x.Name, Constants.Roles.Admin));
                var userRole = dbContext.Roles.SingleOrDefault(x => string.Equals(x.Name, Constants.Roles.User));
                var guestRole = dbContext.Roles.SingleOrDefault(x => string.Equals(x.Name, Constants.Roles.Guest));

                dbContext.UserRoles.AddRange(new UserRole[]
                {
                    new UserRole()
                    {
                        UserId = userCarlos!.Id,
                        RoleId = adminRole!.Id
                    },
                    new UserRole()
                    {
                        UserId = userAna!.Id,
                        RoleId = adminRole!.Id
                    },
                    new UserRole()
                    {
                        UserId = userRosa!.Id,
                        RoleId = userRole!.Id
                    },
                    new UserRole()
                    {
                        UserId = userLuis!.Id,
                        RoleId = userRole!.Id
                    },
                    new UserRole()
                    {
                        UserId = userAntonio!.Id,
                        RoleId = guestRole!.Id
                    }
                });
                dbContext.SaveChanges();
            }
        }

        private static void SeedPermissions(AppDbContext dbContext)
        {
            if (!dbContext.Permissions.Any())
            {
                dbContext.Permissions.AddRange(new Permission[]
                {
                    new Permission() { Name = Constants.Permissions.Write },
                    new Permission() { Name = Constants.Permissions.Read },
                    new Permission() { Name = Constants.Permissions.Edit },
                    new Permission() { Name = Constants.Permissions.Delete }
                });
                dbContext.SaveChanges();
            }
        }

        private static void SeedRoles(AppDbContext dbContext)
        {
            if (!dbContext.Roles.Any())
            {
                dbContext.Roles.AddRange(new Role[]
                {
                    new Role() { Name = Constants.Roles.Admin },
                    new Role() { Name = Constants.Roles.User },
                    new Role() { Name = Constants.Roles.Guest }
                });
                dbContext.SaveChanges();
            }
        }

        private static void SeedUsers(AppDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                dbContext.Users.AddRange(new User[]
                {
                    new User()
                    {
                        Email = "carlosduarte.1@hotmail.com",
                        PasswordHash = Utils.GetSha256Hash(Constants.DefaultUserPassword),
                        FirstName = "Carlos Enrique",
                        LastName = "Duarte Ortiz"
                    },
                    new User()
                    {
                        Email = "ana@hotmail.com",
                        PasswordHash = Utils.GetSha256Hash(Constants.DefaultUserPassword),
                        FirstName = "Ana",
                        LastName = "López"
                    },
                    new User()
                    {
                        Email = "rosa@hotmail.com",
                        PasswordHash = Utils.GetSha256Hash(Constants.DefaultUserPassword),
                        FirstName = "Rosa",
                        LastName = "Avellaneda"
                    },
                    new User()
                    {
                        Email = "luis@hotmail.com",
                        PasswordHash = Utils.GetSha256Hash(Constants.DefaultUserPassword),
                        FirstName = "Luis",
                        LastName = "Fernández"
                    },
                    new User()
                    {
                        Email = "antonio@hotmail.com",
                        PasswordHash = Utils.GetSha256Hash(Constants.DefaultUserPassword),
                        FirstName = "Antonio",
                        LastName = "Guajardo"
                    }
                });
                dbContext.SaveChanges();
            }
        }
    }
}
