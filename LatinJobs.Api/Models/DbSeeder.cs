using LatinJobs.Api.Entities;
using LatinJobs.Api.Util;

namespace LatinJobs.Api.Models
{
    public static class DbSeeder
    {
        public static void DbSeeding(AppDbContext dbContext)
        {
            SeedUsers(dbContext);
        }

        private static void SeedUsers(AppDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                User[] userArray = new User[] 
                {
                    new User() 
                    {
                        Email = "carlosduarte.1@hotmail.com",
                        PasswordHash = AppUtil.ToSha256("12345678"),
                        FirstName = "Carlos Enrique",
                        LastName = "Duarte Ortiz"
                    },
                    new User() 
                    {
                        Email = "ana@hotmail.com",
                        PasswordHash = AppUtil.ToSha256("12345678"),
                        FirstName = "Ana",
                        LastName = "López"
                    },
                    new User() 
                    {
                        Email = "rosa@hotmail.com",
                        PasswordHash = AppUtil.ToSha256("12345678"),
                        FirstName = "Rosa",
                        LastName = "Avellaneda"
                    },
                    new User() 
                    {
                        Email = "luis@hotmail.com",
                        PasswordHash = AppUtil.ToSha256("12345678"),
                        FirstName = "Luis",
                        LastName = "Fernández"
                    },
                    new User() 
                    {
                        Email = "antonio@hotmail.com",
                        PasswordHash = AppUtil.ToSha256("12345678"),
                        FirstName = "Antonio",
                        LastName = "Guajardo"
                    }
                };
                foreach (User user in userArray)
                {
                    dbContext.Users.Add(user);
                }
                dbContext.SaveChanges();
            }
        }
    }
}
