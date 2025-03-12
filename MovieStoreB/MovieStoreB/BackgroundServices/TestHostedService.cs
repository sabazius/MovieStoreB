namespace MovieStoreB.BackgroundServices
{
    public class TestHostedService : IHostedService
    {
        private readonly ILogger<TestHostedService> _logger;

        public TestHostedService(ILogger<TestHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TestHostedService is starting.");

            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation($"Do work...{DateTime.Now}");

                    await Task.Delay(1000, cancellationToken);
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TestHostedService is stopping.");

            return Task.CompletedTask;
        }
    }
}
