using Microsoft.AspNetCore.Mvc;
using MovieStoreB.Models.DTO;
using MovieStoreB.Models.Responses;

namespace ActorBioAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActorDataController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ActorDataController> _logger;

        public ActorDataController(ILogger<ActorDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetBioByActorId")]
        public ActorBioResponse GetBioByActorId(string actorId)
        {
            return new ActorBioResponse
            {
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }

        [HttpPost(Name = "GetBioByActor")]
        public ActorBioResponse GetBioByActor([FromBody] Actor actor)
        {
            return new ActorBioResponse
            {
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }
    }
}
