using MovieStoreB.BL.Interfaces;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.BL.Services
{
    internal class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public List<Movie> GetMovies()
        {
            return _movieRepository.GetMovies();
        }

        public void AddMovie(Movie movie)
        {
            if (movie == null || movie.Actors == null) return;

            foreach (var actor in movie.Actors)
            {
                if (!Guid.TryParse(actor, out _)) return;
            }

            _movieRepository.AddMovie(movie);
        }

        public void DeleteMovie(string id)
        {
            if (!string.IsNullOrEmpty(id)) return;

            _movieRepository.DeleteMovie(id);
        }

        public Movie? GetMoviesById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return null;
            }

            return _movieRepository.GetMoviesById(id);
        }
    }
}
