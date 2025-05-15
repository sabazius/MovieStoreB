using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieStoreB.DL.Cache;
using MovieStoreB.Models.Configurations;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Repositories.MongoRepositories
{
    internal class ComposerRepository : ICacheRepository<int, Composer>
    {
        private readonly IMongoCollection<Composer> _actorsCollection;
        private readonly ILogger<ComposerRepository> _logger;

        public ComposerRepository(ILogger<ComposerRepository> logger, IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            _logger = logger;

            if (string.IsNullOrEmpty(mongoConfig?.CurrentValue?.ConnectionString) || string.IsNullOrEmpty(mongoConfig?.CurrentValue?.DatabaseName))
            {
                _logger.LogError("MongoDb configuration is missing");

                throw new ArgumentNullException("MongoDb configuration is missing");
            }

            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);

            _actorsCollection = database.GetCollection<Composer>($"{nameof(Composer)}s");
        }


        public async Task<IEnumerable<Composer?>> DifLoad(DateTime lastExecuted)
        {
            var result = await _actorsCollection.FindAsync(m => m.DateInserted >= lastExecuted);

            return await result.ToListAsync();
        }

        public async Task<IEnumerable<Composer?>> FullLoad()
        {
            return await GetAllActors();
        }

        public async Task<IEnumerable<Composer?>> GetAllActors()
        {
            var result = await _actorsCollection.FindAsync(m => true);

            return await result.ToListAsync();
        }

        public async Task<Composer?> GetById(int id)
        {
            var result = await _actorsCollection.FindAsync(m => m.Id == id);

            return await result.FirstOrDefaultAsync();
        }
    }
}
