using Microsoft.EntityFrameworkCore;
using OfficesManager.API.Services;
using OfficesManager.Contracts.IRepoitories;
using OfficesManager.Contracts.IServices;
using FluentValidation;
using OfficesManager.Database;
using OfficesManager.Database.Repositories;
using System.Reflection;
using OfficesManager.API.Validators;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

namespace OfficesManager.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(optionss =>
            {
                optionss.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<OfficesManagerDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("SqlConnection"), b =>
                b.MigrationsAssembly("OfficesManager.Database")));
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IOfficesService, OfficesService>();
            services.AddScoped<IOfficesRepository, OfficesRepository>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<OfficeForCreationDtoValidator>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Name = "Bearer"
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
