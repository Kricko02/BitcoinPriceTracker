using BitcoinPriceTracker.Data;
using BitcoinPriceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace BitcoinPriceTracker.Seeders
{
    public class BitcoinPriceSeeder
    {
        private readonly ApplicationDbContext _context;

        public BitcoinPriceSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            
            if (!await _context.BitcoinPrices.AnyAsync())
            {
                var random = new Random();
                var now = DateTime.UtcNow;

             
                for (int day = 0; day < 14; day++) 
                {
                    for (int hour = 0; hour < 24; hour++) 
                    {
                        var dateTime = now.AddDays(-day).AddHours(-hour);
                        var price = random.NextDouble() * (50000 - 40000) + 40000;

                        var bitcoinPrice = new BitcoinPrice
                        {
                            DateTime = dateTime,
                            Price = price
                        };

                        _context.BitcoinPrices.Add(bitcoinPrice);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
