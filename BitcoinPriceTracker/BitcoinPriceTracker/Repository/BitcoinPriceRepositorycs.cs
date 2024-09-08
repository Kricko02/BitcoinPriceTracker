using BitcoinPriceTracker.Data;
using BitcoinPriceTracker.Interfaces;
using BitcoinPriceTracker.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;



namespace BitcoinPriceTracker.Repository
{
    public class BitcoinPriceRepositorycs : IBitcoinPrice
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;
        public BitcoinPriceRepositorycs(HttpClient httpClient, ApplicationDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
          
        }


        public async Task<double> GetPriceAsync()
        {
            var response = await _httpClient.GetStringAsync("https://api.coinlore.net/api/ticker/?id=90");
            var jsonArray = JArray.Parse(response);
            var priceUsd = jsonArray[0]["price_usd"].Value<string>();

            return double.Parse(priceUsd);
        }

        public async Task SavePriceAsync(double price)
        {
            var bitcoinPrice = new BitcoinPrice
            {
                DateTime = DateTime.UtcNow,
                Price = price
            };

            _context.BitcoinPrices.Add(bitcoinPrice);
            await _context.SaveChangesAsync();
        }

        public async Task<double?> GetAveragePriceForDateAsync(DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1).AddTicks(-1);

            var prices = await _context.BitcoinPrices.Where(bp => bp.DateTime >= startOfDay && bp.DateTime <= endOfDay).Select(bp => bp.Price).ToListAsync();

            if (prices.Any())
            {
                return prices.Average();
               
            }
            else
            {
                return null;
            }
        }

    }
}
