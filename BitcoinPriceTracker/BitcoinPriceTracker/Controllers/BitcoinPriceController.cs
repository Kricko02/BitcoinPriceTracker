using BitcoinPriceTracker.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BitcoinPriceTracker.Controllers
{
    [Route("api/btc")]
    [ApiController]
    public class BitcoinPriceController : ControllerBase
    {
        private readonly IBitcoinPrice _bitcoinPrice;
        public BitcoinPriceController(IBitcoinPrice bitcoinPrice)
        {
            _bitcoinPrice = bitcoinPrice;
        }

        [HttpGet("GetCurrentPrice")]
        public async Task<IActionResult> GetCurrentPrice()
        {
            var price = await _bitcoinPrice.GetPriceAsync();

            return Ok(price);
        }

        [HttpGet("GetAveragePriceForDate")]
        public async Task<IActionResult> GetAveragePriceForDate([FromQuery] DateTime date)
        {
            var avg_price = await _bitcoinPrice.GetAveragePriceForDateAsync(date);

            if (avg_price != null)
            {
                return Ok(avg_price);
            }
            else { return BadRequest("There is no data for that date"); }
            
        }
    }
}
