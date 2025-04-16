using Microsoft.Extensions.DependencyInjection;
using MovieStoreB.DL.Cache;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.DL.Repositories;
using MovieStoreB.DL.Repositories.MongoRepositories;

namespace MovieStoreB.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection 
            AddDataDependencies(
                this IServiceCollection services)
        {
            services.AddSingleton<IMovieRepository, MoviesRepository>();
            services.AddSingleton<IActorRepository, ActorMongoRepository>();

            services.AddHostedService<MongoCacheDistributor>();

            return services;
        }
    }
}
