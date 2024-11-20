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
            if (movie == null) return;

            _movieRepository.AddMovie(movie);
        }

        public void DeleteMovie(int id)
        {
            if (id <= 0) return;

            _movieRepository.DeleteMovie(id);
        }

        public Movie? GetMoviesById(int id)
        {
            if (id <= 0) return null;

            return _movieRepository.GetMoviesById(id);
        }
    }
}
