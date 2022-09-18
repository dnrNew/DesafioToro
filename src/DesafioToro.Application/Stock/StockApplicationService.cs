using DesafioToro.Application.Dtos;
using DesafioToro.Application.Helpers;
using DesafioToro.Domain.Stock;

namespace DesafioToro.Application.Services
{
    public class StockApplicationService : IStockApplicationService
    {
        private readonly IStockRepository _stockRepository;

        public StockApplicationService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<List<StockDto>> GetStocks()
        {
            var stocks = await _stockRepository.GetStocks();
            var stocksDto = stocks.Select(s => StockHelper.ToDto(s)).ToList();

            return stocksDto;
        }
    }
}
