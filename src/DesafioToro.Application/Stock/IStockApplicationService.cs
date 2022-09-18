using DesafioToro.Application.Dtos;

namespace DesafioToro.Application.Services
{
    public interface IStockApplicationService
    {
        Task<List<StockDto>> GetStocks();
    }
}
