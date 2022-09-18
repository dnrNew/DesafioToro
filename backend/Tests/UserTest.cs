using DesafioToro.Domain.Stocks;
using DesafioToro.Domain.UserAssets;
using DesafioToro.Domain.Users;
using Shouldly;

namespace Tests
{
    public class UserTest
    {
        [Fact]
        public void User_ExecuteOrder_ReturnErrorMessage()
        {
            Should.NotThrow(() =>
            {
                var user = new User();
                user.Balance = 100.00M;
                
                var (success, message) = user.ExecuteOrder(2, 1, 100.00M);

                success.ShouldBeFalse();
                message.ShouldBe("Saldo Insuficiente");
            });
        }

        [Fact]
        public void User_ExecuteOrder_ReturnSuccessMessage()
        {
            Should.NotThrow(() =>
            {
                var stock = new Stock() { CurrentPrice = 4.35M, Symbol = "MGLU3" };
                var user = new User();
                user.Balance = 500.00M;
                user.UserAssets = new List<UserAsset>() {
                    new UserAsset() {
                        StockId = 1,
                        Quantity = 5,
                        Stock = stock
                    }
                };

                var (success, message) = user.ExecuteOrder(10, stock.Id, stock.CurrentPrice);

                success.ShouldBeTrue();
                message.ShouldBe("Ordem executada com sucesso");
            });
        }
    }
}