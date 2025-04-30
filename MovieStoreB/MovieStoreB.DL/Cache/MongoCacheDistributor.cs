using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Cache
{
    public class MongoCachePopulator<TData, TDataRepository, TConfigurationType, TKey> : BackgroundService 
        where TDataRepository : ICacheRepository<TData>
        where TData : CacheItem<TKey>
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

                if (updatedMovies == null || !updatedMovies.Any())
                {
                    continue;
                }

                var lastUpdated = updatedMovies.Last()?.DateInserted;

                lastExecuted = lastUpdated ?? DateTime.UtcNow;

            }
        }
    }
}
