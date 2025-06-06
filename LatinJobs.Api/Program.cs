using AspNetCoreRateLimit;
using LatinJobs.Api.Entities;
using LatinJobs.Api.Entities.Interfaces;
using LatinJobs.Api.Mapping;
using LatinJobs.Api.Middlewares;
using LatinJobs.Api.Models;
using LatinJobs.Api.Repositories;
using LatinJobs.Api.Repositories.Interfaces;
using LatinJobs.Api.Services;
using LatinJobs.Api.Services.Interfaces;
using LatinJobs.Api.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;
using ILogger = Serilog.ILogger;

namespace LatinJobs.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Environments
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            // Logger
            try
            {
                var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }
                string logFilePath = Path.Combine(logDirectory, "log.txt");
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .WriteTo.Console()
                    .WriteTo.File(logFilePath)
                    .CreateLogger();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting up logger: {ex.Message}");
            }

            // IpRateLimit
            builder.Services.AddMemoryCache();
            builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
            builder.Services.AddInMemoryRateLimiting();
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });

            // Mapping rules
            MappingConfig.RegisterMappings();

            // Repositories
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IRoleRepository, RoleRepository>();
            builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();
            builder.Services.AddTransient<IPermissionRepository, PermissionRepository>();
            builder.Services.AddTransient<IRolePermissionRepository, RolePermissionRepository>();
            builder.Services.AddTransient<IUserAuthenticationRepository, UserAuthenticationRepository>();
            builder.Services.AddTransient<IJobRepository, JobRepository>();

            // Services
            builder.Services.AddTransient<IJwtService, JwtService>();
            builder.Services.AddTransient<IHasPermissionService, HasPermissionService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IRoleService, RoleService>();
            builder.Services.AddTransient<IUserRoleService, UserRoleService>();
            builder.Services.AddTransient<IPermissionService, PermissionService>();
            builder.Services.AddTransient<IRolePermissionService, RolePermissionService>();
            builder.Services.AddTransient<IUserAuthenticationService, UserAuthenticationService>();
            builder.Services.AddTransient<IJobService, JobService>();

            // DB Context
            builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options => 
            {
                string dbConnectionString = builder.Configuration.GetConnectionString(Constants.ConnectionStringName)!;
                var serverVersion = new MariaDbServerVersion(new Version(11, 7, 2)); // MariaDB 11.7.2
                options.UseMySql(dbConnectionString, serverVersion, dbOptions => 
                {
                    dbOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name);
                });
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddSingleton<ILogger>(Log.Logger);

            var app = builder.Build();
            
            app.UseIpRateLimiting();

            app.UseCors(options =>
            {
                options.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin()
                       .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
            });

            // Data seeding
            using (IServiceScope scope = app.Services.CreateScope())
            {
                try
                {
                    var dbContext = (AppDbContext)scope.ServiceProvider.GetService<IAppDbContext>()!;
                    dbContext.Database.Migrate();
                    DbSeeder.DoSeeding(dbContext);
                }
                catch (Exception ex)
                {
                    ILogger<Program> logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or initializing the database");
                }
            }

            if (app.Environment.IsDevelopment())
            {                
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseLogger();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}