using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Repositories.MongoRepositories
{
    internal class ActorMongoRepository : IActorRepository
    {
        public Actor? GetById(string id)
        {
            return new Actor
            {
                Id = id,
                Name = "Test Actor"
            };
        }
    }
}
