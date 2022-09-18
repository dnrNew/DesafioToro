using DesafioToro.Application.Dtos;
using DesafioToro.Domain.Stocks;

namespace DesafioToro.Application.Helpers
{
    public static class StockHelper
    {
        public static StockDto ToDto(Stock stock)
        {
            return new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CurrentPrice = stock.CurrentPrice
            };
        }
    }
}
