using MovieStoreB.BL.Interfaces;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.BL.Services
{
    internal class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;

        public MovieService(IMovieRepository movieRepository, IActorRepository actorRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
        }

        public async Task<List<Movie>> GetMovies()
        {
            return await _movieRepository.GetMovies();
        }

        public async Task AddMovie(Movie movie)
        {
            if (movie == null || movie.Actors == null) return;

            foreach (var actor in movie.Actors)
            {
                if (!Guid.TryParse(actor, out _)) return;
            }

            await _movieRepository.AddMovie(movie);
        }

        public async Task DeleteMovie(string id)
        {
            if (!string.IsNullOrEmpty(id)) return;

            await _movieRepository.DeleteMovie(id);
        }

        public async Task<Movie?> GetMoviesById(string id)
        {
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var movieId))
            {
                return null;
            }

            return await _movieRepository.GetMoviesById(movieId.ToString());
        }

        public async Task AddActor(string movieId, Actor actor)
        {
            if (string.IsNullOrEmpty(movieId) || actor == null) return;

            if (!Guid.TryParse(movieId, out _)) return;

            var movie = await _movieRepository.GetMoviesById(movieId);

            if (movie == null) return;

            if (movie.Actors == null)
            {
                movie.Actors = new List<string>();
            }

            if (actor.Id == null || string.IsNullOrEmpty(actor.Id) || Guid.TryParse(actor.Id, out _) == false) return;

            var existingActor = await _actorRepository.GetById(actor.Id);

            if (existingActor != null) return;

            movie.Actors.Add(actor.Id);
        }
    }
}
