using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.DL.Repositories.MongoRepositories;

namespace MovieStoreB.DL.Cache
{
    //must be in separate project and deployable service
    // Generic Params -> TData, TDataRepository
    //public class MongoCacheDistributor : BackgroundService
    //{
    //    private readonly IMovieRepository _movieRepository;

    //    public MongoCacheDistributor(IMovieRepository movieRepository)
    //    {
    //        _movieRepository = movieRepository;
    //    }

    //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //    {
    //        var lastExecuted = DateTime.UtcNow;

    //        var result = _movieRepository.GetMovies();

    //        while (!stoppingToken.IsCancellationRequested)
    //        {
    //            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);

    //            var updatedMovies = await _movieRepository.GetMoviesAfterDateTime(lastExecuted);

    //            lastExecuted = DateTime.UtcNow;
    //        }
    //    }
    //}

    public class MongoCachePopulator<TData, TDataRepository, TConfigurationType> : BackgroundService 
        where TDataRepository : ICacheRepository<TData>
        where TData : class
        where TConfigurationType : CacheConfiguration
    {
        private readonly ICacheRepository<TData> _cacheRepository;
        private readonly IOptionsMonitor<TConfigurationType> _configuration;

        public MongoCachePopulator(ICacheRepository<TData> cacheRepository, IOptionsMonitor<TConfigurationType> configuration)
        {
            _cacheRepository = cacheRepository;
            _configuration = configuration;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var lastExecuted = DateTime.UtcNow;

            var result = await _cacheRepository.FullLoad();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(_configuration.CurrentValue.RefreshInterval), stoppingToken);

                var updatedMovies = await _cacheRepository.DifLoad(lastExecuted);

                lastExecuted = DateTime.UtcNow;
            }
        }
    }
}
