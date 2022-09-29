using DesafioToro.Application.Order.Dtos;
using DesafioToro.Application.Services;
using DesafioToro.Domain.Stocks;
using DesafioToro.Domain.Users;
using Moq;
using Shouldly;

namespace Tests
{
    public class OrderApplicationServiceTest
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IStockRepository> _stockRepositoryMock;
        private OrderApplicationService _orderAppService;

        public OrderApplicationServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _stockRepositoryMock = new Mock<IStockRepository>();
            _orderAppService = new OrderApplicationService(_userRepositoryMock.Object, _stockRepositoryMock.Object);
        }

        private void MockOrderRepositories(User user, Stock stock)
        {
            _userRepositoryMock.Setup(w => w.GetUser(It.IsAny<int>())).ReturnsAsync(user);
            _stockRepositoryMock.Setup(w => w.GetStockBySymbol(It.IsAny<string>())).ReturnsAsync(stock);
        }

        [Fact]
        public void ExecuteOrder_AddUserAsset_ReturnMessageOrderSuccess()
        {
            Should.NotThrow(() =>
            {
                var order = new OrderDto() { Amount = 3, Symbol = "MGLU3", UserId = 1 };
                var user = new User() { Id = 1, Name = "Shiryu", Cpf = "29247119081", Account = "300123", Balance = 5000 };
                var stock = new Stock() { Id = 1, CurrentPrice = 4.46M, Symbol = "MGLU3" };

                MockOrderRepositories(user, stock);

                var orderResult = _orderAppService.ExecuteOrder(order);

                orderResult.ShouldNotBeNull();
                orderResult.Result.Success.ShouldBeTrue();
                orderResult.Result.Message.ShouldBe("Ordem executada com sucesso");
            });
        }

        [Fact]
        public void ExecuteOrder_BalanceOrderInsufficient_ReturnBalanceInsufficientMessage()
        {
            Should.NotThrow(() =>
            {
                var order = new OrderDto() { Amount = 1000, Symbol = "MGLU3", UserId = 1 };
                var user = new User() { Id = 1, Name = "Shiryu", Cpf = "29247119081", Account = "300123", Balance = 3000 };
                var stock = new Stock() { Id = 1, CurrentPrice = 4.46M, Symbol = "MGLU3" };

                MockOrderRepositories(user, stock);

                var orderResult = _orderAppService.ExecuteOrder(order);

                orderResult.ShouldNotBeNull();
                orderResult.Result.Success.ShouldBeFalse();
                orderResult.Result.Message.ShouldBe("Saldo Insuficiente");
            });
        }

        [Fact]
        public void ExecuteOrder_StockNotExist_ReturnMessageAssetInvalid()
        {
            Should.NotThrow(() =>
            {
                var order = new OrderDto() { Amount = 3, Symbol = "FAKE3", UserId = 1 };
                var user = new User() { Id = 1, Name = "Shiryu", Cpf = "29247119081", Account = "300123", Balance = 5000 };
                var stock = new Stock() { Symbol = "FAKE3" };

                MockOrderRepositories(user, stock);

                var orderResult = _orderAppService.ExecuteOrder(order);

                orderResult.ShouldNotBeNull();
                orderResult.Result.Success.ShouldBeFalse();
                orderResult.Result.Message.ShouldBe("Ativo Inválido");
            });
        }
    }
}
