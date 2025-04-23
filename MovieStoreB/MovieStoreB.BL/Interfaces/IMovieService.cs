using MovieStoreB.Models.DTO;

namespace MovieStoreB.BL.Interfaces
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMovies();

        void AddMovie(Movie movie);

        void DeleteMovie(string id);

        Movie? GetMoviesById(string id);

        void AddActor(string movieId, Actor actor);
    }
}
