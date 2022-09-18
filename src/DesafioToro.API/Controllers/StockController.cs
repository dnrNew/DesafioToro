using DesafioToro.Application.Dtos;
using DesafioToro.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioToro.Api.Controllers
{
    [ApiController]
    [Route("trends")]
    public class StockController : ControllerBase
    {
        private IStockApplicationService _stockAppService;

        public StockController(IStockApplicationService stockAppService)
        {
            _stockAppService = stockAppService;
        }

        [HttpGet(Name = "GetStocks")]
        public async Task<List<StockDto>> GetStocks()
        {
            var stocks = await _stockAppService.GetStocks();

            return stocks;
        }
    }
}