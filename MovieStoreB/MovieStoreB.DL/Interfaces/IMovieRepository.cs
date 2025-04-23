
using MovieStoreB.DL.Cache;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Interfaces
{
    public interface IMovieRepository : ICacheRepository<Movie>
    {
        Task<List<Movie>> GetMovies();

        void AddMovie(Movie movie);

        void DeleteMovie(string id);

        Movie? GetMoviesById(string id);

        Task<IEnumerable<Movie?>> GetMoviesAfterDateTime(DateTime date);
    }
}
