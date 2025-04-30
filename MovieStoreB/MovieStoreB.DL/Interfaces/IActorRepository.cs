using MovieStoreB.DL.Cache;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Interfaces
{
    public interface IActorRepository : ICacheRepository<Actor>
    {
        Task<Actor?> GetById(string id);
    }
}
