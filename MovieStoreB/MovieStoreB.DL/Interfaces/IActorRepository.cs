using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Interfaces
{
    public interface IActorRepository
    {
        Task<Actor?> GetById(string id);

        Task<List<Actor>> GetActors(List<string> actorIds);
    }
}
