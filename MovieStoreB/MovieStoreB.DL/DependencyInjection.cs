using Microsoft.Extensions.DependencyInjection;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.DL.Repositories;

namespace MovieStoreB.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection 
            AddDataDependencies(
                this IServiceCollection services)
        {
            services.AddSingleton
            <IMovieRepository, MovieRepository>();
           
            return services;
        }
    }
}
