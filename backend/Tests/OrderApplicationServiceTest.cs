using DesafioToro.Domain.Users;
using Shouldly;

namespace Tests
{
    public class OrderApplicationServiceTest
    {
        [Fact]
        public void Order_ExecuteOrder_ShouldReturnBalanceInsufficientMessage()
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
    }
}
