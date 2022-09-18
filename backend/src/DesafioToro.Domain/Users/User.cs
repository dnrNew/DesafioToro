using DesafioToro.Domain.UserAssets;

namespace DesafioToro.Domain.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Account { get; set; }
        public decimal Balance { get; set; }
        public List<UserAsset> UserAssets = new List<UserAsset>();

        public (bool, string) ExecuteOrder(int amount, int stockId, decimal currentPrice)
        {
            var orderPrice = amount * currentPrice;

            if (orderPrice > this.Balance)
            {
                return (false, "Saldo Insuficiente");
            }

            this.Balance = this.Balance - orderPrice;
            var asset = this.UserAssets.Find(w => w.StockId == stockId);

            if (asset != null)
            {
                asset.Quantity += amount;
            }
            else
            {
                UserAssets.Add(new UserAsset()
                {
                    Quantity = amount,
                    StockId = stockId,
                    UserId = this.Id
                });
            }

            return (true, "Ordem executada com sucesso");
        }
    }
}
