namespace DesafioToro.Domain.Stocks
{
    public interface IStockRepository : IDisposable
    {
        Task<List<Stock>> GetStocks();
        Task<Stock> GetStockBySymbol(string symbol);
    }
}
