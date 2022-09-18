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
        public async Task<IActionResult> ExecuteOrder()
        {
            var success = await _orderAppService.ExecuteOrder(new OrderDto());

            if (!success)
                return BadRequest("Erro");

            return Ok();
        }
    }
}