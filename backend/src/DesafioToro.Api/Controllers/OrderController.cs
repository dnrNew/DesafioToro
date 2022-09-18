using DesafioToro.Application.Order.Dtos;
using DesafioToro.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioToro.Api.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private IOrderApplicationService _orderAppService;

        public OrderController(IOrderApplicationService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        [HttpPost(Name = "ExecuteOrder")]
        public async Task<IActionResult> ExecuteOrder(OrderMinDto order)
        {
            var headers = Request.Headers;
            int userId = 0;

            if (headers.ContainsKey("userId"))
                int.Parse(headers["userId"].ToString());

            var executeOrder = new OrderDto()
            {
                Symbol = order.Symbol,
                Amount = order.Amount,
                UserId = userId
            };

            var result = await _orderAppService.ExecuteOrder(executeOrder);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}