using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieStoreB.DL.Cache;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.DL.Kafka;
using MovieStoreB.DL.Repositories.MongoRepositories;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection
            AddDataDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IMovieRepository, MoviesRepository>();
            services.AddSingleton<IActorRepository, ActorMongoRepository>();

            //services.AddHostedService<MongoCacheDistributor>();
            //services.AddSingleton<ICacheRepository<Movie>, MoviesRepository>();

            services.AddCache<MoviesCacheConfiguration, MoviesRepository, Movie, string>(config);

            //services.AddHostedService<MongoCachePopulator<Movie, IMovieRepository>>();

            return services;
        }

        public static IServiceCollection AddCache<TCacheConfiguration, TCacheRepository, TData, TKey>(this IServiceCollection services, IConfiguration config)
           where TCacheConfiguration : CacheConfiguration
           where TCacheRepository : class, ICacheRepository<TData>
           where TData : CacheItem<TKey>
           where TKey : notnull
        {
            var configSection = config.GetSection(typeof(TCacheConfiguration).Name);

            if (!configSection.Exists())
            {
                throw new ArgumentNullException(typeof(TCacheConfiguration).Name, "Configuration section is missing in appsettings!");
            }

            services.Configure<TCacheConfiguration>(configSection);

            services.AddSingleton<ICacheRepository<TData>, TCacheRepository>();
            services.AddSingleton<IKafkaProducer<TData>, KafkaProducer<TKey, TData>>();
            services.AddHostedService<MongoCachePopulator<TData, ICacheRepository<TData>, TCacheConfiguration, TKey>>();

            return services;
        }
    }

    public class MoviesCacheConfiguration : CacheConfiguration
    {
    }

    public class CacheConfiguration
    {
        public string Topic { get; set; } = string.Empty;

        public int RefreshInterval { get; set; } = 30;
    }
}
