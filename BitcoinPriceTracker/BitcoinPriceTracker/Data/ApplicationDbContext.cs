using BitcoinPriceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace BitcoinPriceTracker.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<BitcoinPrice> BitcoinPrices { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
    }
}
