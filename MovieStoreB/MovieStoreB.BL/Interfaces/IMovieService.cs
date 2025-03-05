using MovieStoreB.Models.DTO;

namespace MovieStoreB.BL.Interfaces
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMovies();

        Task AddMovie(Movie movie);

        Task DeleteMovie(string id);

        Task<Movie?> GetMoviesById(string id);

        Task AddActor(string movieId, Actor actor);
    }
}
