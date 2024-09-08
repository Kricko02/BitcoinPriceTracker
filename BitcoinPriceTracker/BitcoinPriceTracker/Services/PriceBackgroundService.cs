
using BitcoinPriceTracker.Interfaces;
using BitcoinPriceTracker.Repository;

namespace BitcoinPriceTracker.Services
{
    public class PriceBackgroundService : BackgroundService
    {
        private readonly ILogger<PriceBackgroundService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PriceBackgroundService(ILogger<PriceBackgroundService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var bitcoinPrice = scope.ServiceProvider.GetRequiredService<IBitcoinPrice>();

                    _logger.LogInformation("Fetching BTC price...");
                    var price = await bitcoinPrice.GetPriceAsync();
                    _logger.LogInformation($"Current BTC price: {price}");

                    await bitcoinPrice.SavePriceAsync(price);
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
