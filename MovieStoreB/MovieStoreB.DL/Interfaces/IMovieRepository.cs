
using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Interfaces
{
    public interface IMovieRepository
    {
        List<Movie> GetMovies();
    }
}
