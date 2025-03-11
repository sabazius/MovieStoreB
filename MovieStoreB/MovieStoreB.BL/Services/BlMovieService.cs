using MovieStoreB.BL.Interfaces;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;
using MovieStoreB.Models.Responses;

namespace MovieStoreB.BL.Services
{
    internal class BlMovieService : IBlMovieService
    {
        private readonly IMovieService _movieService;
        private readonly IActorRepository _actorRepository;

        public BlMovieService(IMovieService movieService, IActorRepository actorRepository)
        {
            _movieService = movieService;
            _actorRepository = actorRepository;
        }

        public async Task<List<FullMovieDetails>> GetAllMovieDetails()
        {
            var result = new List<FullMovieDetails>();

            var movies = await _movieService.GetMovies();

            foreach (var movie in movies)
            {
                var movieDetails = new FullMovieDetails();
                movieDetails.Title = movie.Title;
                movieDetails.Year = movie.Year;
                movieDetails.Id = movie.Id;
                
                var actors = await
                    _actorRepository.GetActors(movie.Actors);

                movieDetails.Actors = actors;
                result.Add(movieDetails);
            }
            return result;
        }
    }
}
