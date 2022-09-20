using DesafioToro.Domain.Stocks;
using DesafioToro.Domain.UserAssets;
using DesafioToro.Domain.Users;
using Shouldly;

namespace Tests
{
    public class UserTest
    {
        [Fact]
        public void User_ExecuteOrder_ShouldReturnBalanceInsufficientMessage()
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
        public void User_ExecuteOrder_ShouldReturnNewStocksAmount()
        {
            Should.NotThrow(() =>
            {
                var user = new User();
                var stock = new Stock() { Id = 1, Symbol = "MGLU3", CurrentPrice = 4.46M };
                var initialQuantity = 5;
                var addQuantity = 10;
                var initialBalance = user.Balance = 500.00M;
                var finalBalance = initialBalance - addQuantity * stock.CurrentPrice;

                user.UserAssets = new List<UserAsset>() {
                    new UserAsset() {
                        StockId = 1,
                        Quantity = initialQuantity,
                        Stock = stock
                    }
                };

                var (success, message) = user.ExecuteOrder(addQuantity, stock.Id, stock.CurrentPrice);
                var newQuantity = initialQuantity + addQuantity;

                success.ShouldBeTrue();
                message.ShouldBe("Ordem executada com sucesso");
                user.UserAssets.First().Quantity.ShouldBe(newQuantity);
                user.Balance.ShouldBe(finalBalance);
            });
        }

        [Fact]
        public void User_ExecuteOrder_ShouldReturnNewUserAsset()
        {
            Should.NotThrow(() =>
            {
                var user = new User();
                var stock = new Stock() { Id = 1, Symbol = "MGLU3", CurrentPrice = 4.46M };
                var newStock = new Stock() { Id = 2, Symbol = "PETR3", CurrentPrice = 30.78M };
                var addQuantity = 5;
                var initialBalance = user.Balance = 300.00M;
                var finalBalance = initialBalance - addQuantity * newStock.CurrentPrice;

                user.UserAssets = new List<UserAsset>() {
                    new UserAsset() {
                        StockId = 1,
                        Quantity = 15,
                        Stock = stock
                    }
                };

                var (success, message) = user.ExecuteOrder(addQuantity, newStock.Id, newStock.CurrentPrice);
                var newAsset = user.UserAssets.Find(w => w.StockId == newStock.Id);

                success.ShouldBeTrue();
                message.ShouldBe("Ordem executada com sucesso");
                newAsset.ShouldNotBeNull();
                newAsset.Quantity.ShouldBe(addQuantity);
                user.Balance.ShouldBe(finalBalance);
                user.UserAssets.Count().ShouldBe(2);
            });
        }
    }
}