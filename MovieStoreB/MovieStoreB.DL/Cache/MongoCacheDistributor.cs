using Microsoft.Extensions.Hosting;
using MovieStoreB.DL.Interfaces;

namespace MovieStoreB.DL.Cache
{
    //must be in separate ptoject and deployable service
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
}
