using Microsoft.AspNetCore.Mvc;

namespace DesafioToro.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private static readonly string[] Stocks = new[]
        {
            "GOLL4", "MGLU3", "BIDI4", "PETR4", "BBSE3", "ITSA4", "WEGE3", "AERI3", "OIBR3", "ABEV3"
        };

        public StockController()
        {
        }

        [HttpGet(Name = "GetStock")]
        public IEnumerable<Stock> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Stock
            {
                Code = Stocks[Random.Shared.Next(Stocks.Length)]
            })
            .ToArray();
        }
    }
}