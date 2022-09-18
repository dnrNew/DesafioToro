using DesafioToro.Application.Order.Dtos;
using DesafioToro.Domain.Stocks;
using DesafioToro.Domain.Users;

namespace DesafioToro.Application.Services
{
    public class OrderApplicationService : IOrderApplicationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStockRepository _stockRepository;

        public OrderApplicationService(IUserRepository userRepository, IStockRepository stockRepository)
        {
            _userRepository = userRepository;
            _stockRepository = stockRepository;
        }

        public async Task<ResultDto> ExecuteOrder(OrderDto order)
        {
            var user = await _userRepository.GetUser(order.UserId);
            var stock = await _stockRepository.GetStockBySymbol(order.Symbol);

            if (stock.Id <= 0)
                return new ResultDto(false, "Ativo Inválido");

            var (success, message) = user.ExecuteOrder(order.Amount, stock.Id, stock.CurrentPrice);

            if (!success) { 
                return new ResultDto(success, message);
            }

            await _userRepository.SaveUserExecutedOrder(user, stock.Id);

            return new ResultDto(success, message);
        }
    }
}
