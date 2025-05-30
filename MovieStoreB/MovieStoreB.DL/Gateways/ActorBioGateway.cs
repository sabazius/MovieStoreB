using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;
using MovieStoreB.Models.Responses;
using Newtonsoft.Json;
using RestSharp;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;

namespace MovieStoreB.DL.Gateways
{
    internal class ActorBioGateway : IActorBioGateway
    {
        private readonly RestClient _client;

        public ActorBioGateway()
        {
            var options = new RestClientOptions("https://localhost:7077");

            _client = new RestClient(options);
           
            // The cancellation token comes from the caller. You can still make a call without it.
        }

        public async Task<ActorBioResponse> GetBioByActor(Actor actor)
        {
            var request = new RestRequest($"/ActorData", Method.Post);

            var json = JsonConvert.SerializeObject(actor);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            request.AddBody(data);

            var response = await _client.ExecuteAsync<ActorBioResponse>(request);

            //return response.Data;

            return response.Data;
        }

        public async Task<ActorBioResponse> GetBioByActorId(string actorId)
        {
            var request = new RestRequest($"/ActorData", Method.Get);

            request.AddQueryParameter("actorId", actorId);

            var response = await _client.ExecuteAsync<ActorBioResponse>(request);

            return response.Data;
        }
    }
}
