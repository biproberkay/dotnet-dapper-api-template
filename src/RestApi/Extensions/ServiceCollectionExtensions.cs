﻿using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestApi.Application.V1.Aggregates.Users.Handlers;
using RestApi.Application.V1.Services;
using RestApi.Domain.V1.Aggregates.Users.Repositories;
using RestApi.Identity.Configuration;
using RestApi.Identity.Data;
using RestApi.Identity.Services;
using RestApi.Infra.Profiles;
using RestApi.Infra.Services;
using RestApi.Persistence.Repositories;

namespace RestApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });

            return services;
        }
           public static IServiceCollection AddDIConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddScoped<IIdentityService, IdentityService>();

            return services;
        }

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AppIdentityContext>()
                    .AddDefaultTokenProviders();

            services.AddDbContext<AppIdentityContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            return services;
        }
        
        public static IServiceCollection AddEFCoreConfiguration(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<Persistence.Context.AppContext>();

            return services;
        }

        public static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            services.AddFluentValidationAutoValidation();

            return services;
        }

        public static IServiceCollection AddMediatorConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(typeof(LoginQueryHandler).Assembly);
            
            return services;
        }
    }
}
