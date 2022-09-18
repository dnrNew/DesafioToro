namespace DesafioToro.Domain.Stock
{
    public interface IStockRepository : IDisposable
    {
        Task<List<Stock>> GetStocks();
        Task<Stock> GetStockBySymbol(string symbol);
    }
}
