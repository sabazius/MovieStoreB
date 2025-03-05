using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.Configurations;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Repositories.MongoRepositories
{
    internal class ActorRepository : IActorRepository
    {
        private readonly IMongoCollection<Actor> _actorCollection;
        private readonly ILogger<ActorRepository> _logger;

        public ActorRepository(ILogger<ActorRepository> logger, IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            _logger = logger;

            if (string.IsNullOrEmpty(mongoConfig?.CurrentValue?.ConnectionString) || string.IsNullOrEmpty(mongoConfig?.CurrentValue?.DatabaseName))
            {
                _logger.LogError("MongoDb configuration is missing");

                throw new ArgumentNullException("MongoDb configuration is missing");
            }

            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);

            _actorCollection = database.GetCollection<Actor>($"{nameof(Actor)}s");
        }

        public async Task AddActor(Actor movie)
        {
            try
            {
                movie.Id = Guid.NewGuid().ToString();

                await _actorCollection.InsertOneAsync(movie);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task DeleteActor(string id)
        {
            await _actorCollection.DeleteOneAsync(m => m.Id == id);
        }

        public async Task<List<Actor>> GetActors()
        {
            var result =  await _actorCollection.FindAsync(m => true);

            return result.ToList();
        }

        public async Task<Actor?> GetById(string id)
        {
           var result =  await _actorCollection.FindAsync(m => m.Id == id);

           return result.FirstOrDefault();
        }

        public async Task<List<Actor>> GetActors(List<string> actorIds)
        {
            var result = await
                _actorCollection.FindAsync(m => actorIds.Contains(m.Id.ToString()));

            return await result.ToListAsync();
        }
    }
}
