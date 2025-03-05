using Microsoft.Extensions.DependencyInjection;
using MovieStoreB.BL.Interfaces;
using MovieStoreB.BL.Services;

namespace MovieStoreB.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection 
            AddBusinessDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IMovieService, MovieService>();
            services.AddSingleton<IBlMovieService, BlMovieService>();
            return services;
        }
    }
}
