using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Notes.Api.Mappings;
using Notes.Api.Validators;
using Notes.Application.Configurations;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Interfaces.InfrastructureServices;
using Notes.Application.Interfaces.Repositories;
using Notes.Application.Services;
using Notes.Infrastructure;
using Notes.Infrastructure.Decorators;
using Notes.Infrastructure.Repositories;
using Notes.Infrastructure.Services;

namespace Notes.Api.Extensions;

public static class ApplicationExtensions
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
        
        serviceCollection.AddAutoMapper(typeof(MappingProfile));

        serviceCollection.AddScoped<ICategoriesRepository, CategoriesRepository>();
        serviceCollection.AddScoped<INotesRepository, NotesRepository>();
        serviceCollection.AddScoped<IUsersRepository, UsersRepository>();

        serviceCollection.AddScoped<INotesService, NotesService>();
        serviceCollection.AddScoped<INotesService, CachingNotesDecorator>();
        serviceCollection.AddScoped<IUserAuthService, UserAuthService>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddScoped<IPasswordHasherService, BCryptHasherService>();
    }

    public static void AddSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
    }

    public static void ConfigureDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<DataContext>(o =>
            o.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}