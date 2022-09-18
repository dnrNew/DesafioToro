using DesafioToro.Application.Order.Dtos;

namespace DesafioToro.Application.Services
{
    public interface IOrderApplicationService
    {
        Task<ResultDto> ExecuteOrder(OrderDto order);
    }
}
