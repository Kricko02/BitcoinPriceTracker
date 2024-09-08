namespace BitcoinPriceTracker.Interfaces
{
    public interface IBitcoinPrice
    {
        Task<double> GetPriceAsync();
        Task SavePriceAsync(double price);
        Task<double?> GetAveragePriceForDateAsync(DateTime date);
    }
}
