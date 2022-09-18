using DesafioToro.Application.Dtos;
using DesafioToro.Application.Helpers;
using DesafioToro.Domain.Stocks;

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
            await _stockRepository.UpdateCurrentPriceStocks();
            var stocks = await _stockRepository.GetStocks();

            Random random = new Random();
            var stocksRandom = stocks.OrderBy(w => random.Next()).Take(5).ToList();
            var stocksDto = stocksRandom.Select(s => StockHelper.ToDto(s)).ToList();

            return stocksDto;
        }
    }
}
