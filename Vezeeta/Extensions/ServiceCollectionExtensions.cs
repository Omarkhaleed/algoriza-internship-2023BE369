using AutoMapper;
using DomainLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer;
using RepositoryLayer.RepositoryPattern;
using ServicesLayer.Interfaces;
using ServicesLayer.Interfaces.Admin;
using ServicesLayer.Interfaces.Doctor;
using ServicesLayer.Interfaces.Patient;
using ServicesLayer.Mappings;
using ServicesLayer.Mappings.Admin;
using ServicesLayer.Mappings.Doctor;
using ServicesLayer.Mappings.Patient;
using ServicesLayer.Services;
using ServicesLayer.Services.Admin;
using ServicesLayer.Services.Doctor;
using ServicesLayer.Services.Patient;
using System.Text;
using Vezeeta.Filters;
using Vezeeta.Helpers;

namespace Vezeeta.Extensions
{
    public static class ServiceCollectionExtensions
    {


        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserAuthentication, UserAuhentication>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IDoctorManagementService, DoctorManagementService>();
            services.AddScoped<IPatientManagementService, PatientManagementService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
        }
        public static void ConfigureMapping(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            var mapperConfig = new MapperConfiguration(map =>
            {
                map.AddProfile<AuthenticationMappingProfile>();
                map.AddProfile<DiscoundMappigProfile>();
                map.AddProfile<DoctorMappingProfile>();
                map.AddProfile<PatientMappingProfile>();
                map.AddProfile<AuthenticationMappingProfile>();
                map.AddProfile<AuthenticationMappingProfile>();




            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = false;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
          services.AddDbContext<ApplicationDbContext>(
          opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));




        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("JWT");
            var secretKey = jwtConfig["Key"];
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig["Issuer"],
                    ValidAudience = jwtConfig["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }

        public static void ConfigureCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
