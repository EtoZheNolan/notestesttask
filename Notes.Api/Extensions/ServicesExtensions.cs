using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Notes.Api.Mappings;
using Notes.Api.Validators;
using Notes.Application.Enums;
using Notes.Application.Settings;
using Notes.Infrastructure.Settings;

namespace Notes.Api.Extensions;

public static class ServicesExtensions
{
    public static void AddServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

        serviceCollection.AddAutoMapper(typeof(MappingProfile));

        serviceCollection.AddHttpContextAccessor();

        var cacheSettings = configuration.GetSection(nameof(CacheSettings)).Get<CacheSettings>();

        switch (cacheSettings!.CacheType)
        {
            case CacheType.Memory:
                serviceCollection.AddMemoryCache();
                break;
            case CacheType.Redis:
                serviceCollection.AddStackExchangeRedisCache(x =>
                {
                    x.Configuration = cacheSettings.RedisSettings.ConnectionString;
                    x.InstanceName = cacheSettings.RedisSettings.InstanceName;
                });
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static void AddSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        serviceCollection.Configure<CacheSettings>(configuration.GetSection(nameof(CacheSettings)));
    }

    public static void ConfigureAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

        serviceCollection.AddAuthentication(options =>
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
                ValidIssuer = jwtSettings!.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
            };
        });
    }

    public static void ConfigureSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            c.CustomOperationIds(apiDesc =>
            {
                var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                var controllerDisplayName = controllerAction?.ControllerTypeInfo.Name;

                var operationId = controllerDisplayName + "_" + controllerAction?.ActionName;
                return operationId;
            });
        });
    }
}