using MovieStoreB.DL.DB;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Repositories
{
    internal class MovieRepository : IMovieRepository
    {
        public List<Movie> GetMovies()
        {
            return StaticData.Movies;
        }

        public void AddMovie(Movie movie)
        {
            StaticData.Movies.Add(movie);
        }
        public void DeleteMovie(int id)
        {
            if (id <= 0) return;

            var movie = GetMoviesById(id);

            if (movie != null)
            {
                StaticData.Movies.Remove(movie);
            }
        }
        public Movie? GetMoviesById(int id)
        {
            if (id <= 0) return null;

            return StaticData.Movies
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
