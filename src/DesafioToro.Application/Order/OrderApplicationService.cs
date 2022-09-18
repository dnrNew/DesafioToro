using DesafioToro.Application.Order.Dtos;
using DesafioToro.Domain.Stock;
using DesafioToro.Domain.User;

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

            var (success, errorMessage) = user.ExecuteOrder(order.Amount, stock.Id, stock.CurrentPrice);

            if (!success) { 
                return new ResultDto(success, errorMessage);
            }

            await _userRepository.SaveUserExecutedOrder(user, stock.Id);

            return new ResultDto(success, errorMessage);
        }
    }
}
