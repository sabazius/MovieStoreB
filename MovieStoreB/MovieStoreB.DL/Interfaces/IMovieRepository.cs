
using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetMovies();

        Task AddMovie(Movie movie);

        Task DeleteMovie(string id);

        Task<Movie?> GetMoviesById(string id);
    }
}
