using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Interfaces.InfrastructureServices;
using Notes.Application.Interfaces.Repositories;
using Notes.Infrastructure.Repositories;
using Notes.Infrastructure.Services;

namespace Notes.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<ICategoriesRepository, CategoriesRepository>();
        serviceCollection.AddScoped<INotesRepository, NotesRepository>();
        serviceCollection.AddScoped<IUsersRepository, UsersRepository>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddScoped<IPasswordHasherService, BCryptHasherService>();
        serviceCollection.AddScoped<ICurrentUserService, CurrentUserService>();
        
        ConfigureDatabase(serviceCollection, configuration);
    }

    private static void ConfigureDatabase(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<DataContext>(o =>
            o.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}