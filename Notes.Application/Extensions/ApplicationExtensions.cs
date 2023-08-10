using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Decorators;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Services;

namespace Notes.Application.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<INotesService, NotesService>();
        serviceCollection.Decorate<INotesService, CachingNotesDecorator>();
        
        serviceCollection.AddScoped<ICategoriesService, CategoriesService>();
        serviceCollection.AddScoped<IUsersService, UsersService>();
        serviceCollection.AddScoped<IUserAuthService, UserAuthService>();
        serviceCollection.AddScoped<ICacheService, CacheService>();
        serviceCollection.AddScoped<IJsonSerializer, SystemTextJsonSerializer>();
    }
}