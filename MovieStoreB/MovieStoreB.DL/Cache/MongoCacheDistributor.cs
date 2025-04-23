using Microsoft.Extensions.Hosting;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.DL.Repositories.MongoRepositories;

namespace MovieStoreB.DL.Cache
{
    //must be in separate project and deployable service
    // Generic Params -> TData, TDataRepository
    public class MongoCacheDistributor : BackgroundService
    {
        private readonly IMovieRepository _movieRepository;

        public MongoCacheDistributor(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var lastExecuted = DateTime.UtcNow;

            var result = _movieRepository.GetMovies();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);

                var updatedMovies = await _movieRepository.GetMoviesAfterDateTime(lastExecuted);

                lastExecuted = DateTime.UtcNow;
            }
        }
    }

    public class MongoCachePopulator<TData, TDataRepository> : BackgroundService 
        where TDataRepository : ICacheRepository<TData>
        where TData : class
    {
        private readonly ICacheRepository<TData> _cacheRepository;

        public MongoCachePopulator(ICacheRepository<TData> cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var lastExecuted = DateTime.UtcNow;

            var result = await _cacheRepository.FullLoad();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);

                var updatedMovies = await _cacheRepository.DifLoad(lastExecuted);

                lastExecuted = DateTime.UtcNow;
            }
        }
    }
}
