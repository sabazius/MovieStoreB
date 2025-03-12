namespace MovieStoreB.BackgroundServices
{
    public class TestBackgroundService : BackgroundService
    {
        private readonly ILogger<TestBackgroundService> _logger;

        public TestBackgroundService(
            ILogger<TestBackgroundService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            int count = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                    _logger.LogInformation($"Do work...{DateTime.Now}");

                    await Task.Delay(1000, stoppingToken);

                    count++;
                    if (count == 5)
                    {
                        _logger.LogInformation("TestBackgroundService is stopping.");
                        count = 0;
                        throw new AbandonedMutexException();
                    }
                }
                catch (Exception e)
                {
                   _logger.LogError(e, "Error in TestBackgroundService");
                }
            }
        }
    }
}
