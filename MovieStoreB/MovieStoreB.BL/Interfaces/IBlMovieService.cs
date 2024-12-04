using MovieStoreB.Models.Responses;

namespace MovieStoreB.BL.Interfaces
{
    public interface IBlMovieService
    {
        List<FullMovieDetails> GetAllMovieDetails();
    }
}
