using MovieStoreB.Models.Responses;

namespace MovieStoreB.BL.Interfaces
{
    public interface IBlMovieService
    {
        Task<List<FullMovieDetails>> GetAllMovieDetails();
    }
}
