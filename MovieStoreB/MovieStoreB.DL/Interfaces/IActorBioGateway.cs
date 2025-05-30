using MovieStoreB.Models.DTO;
using MovieStoreB.Models.Responses;

namespace MovieStoreB.DL.Interfaces
{
    public interface IActorBioGateway
    {
        Task<ActorBioResponse> GetBioByActorId(string actorId);

        Task<ActorBioResponse> GetBioByActor(Actor actorId);
    }
}