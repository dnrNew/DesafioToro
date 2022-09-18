using DesafioToro.Domain.Stocks;
using DesafioToro.Domain.UserAssets;
using DesafioToro.Domain.Users;
using MySqlConnector;

namespace DesafioToro.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(MySqlConnection connection) : base(connection)
        {
        }

        public async Task<User> GetUser(int userId)
        {
            var user = new User();

            using var command = new MySqlCommand($"SELECT * FROM User WHERE Id = {userId};", _connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                user.Id = reader.GetInt16(0);
                user.Name = reader.GetString(1);
                user.Cpf = reader.GetString(2);
                user.Account = reader.GetString(3);
                user.Balance = reader.GetDecimal(4);
            }

            return user;
        }

        public async Task<List<UserAsset>> GetUserAssets(int userId)
        {
            var userAssets = new List<UserAsset>();

            using var command = new MySqlCommand(
                $"SELECT ua.Id, ua.Quantity, s.Symbol, s.CurrentPrice FROM UserAsset ua\r\n" +
                $"JOIN Stock s ON s.Id = ua.StockId\r\n" +
                $"WHERE ua.UserId = {userId};", _connection);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var userAsset = new UserAsset() { Stock = new Stock() };

                userAsset.Id = reader.GetInt16(0);
                userAsset.Quantity = reader.GetInt16(1);
                userAsset.Stock.Symbol = reader.GetString(2);
                userAsset.Stock.CurrentPrice = reader.GetDecimal(3);

                userAssets.Add(userAsset);
            }

            return userAssets;
        }

        public async Task SaveUserExecutedOrder(User user, int stockId)
        {
            MySqlTransaction transaction = null;

            try
            {
                transaction = await _connection.BeginTransactionAsync();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = _connection;
                cmd.Transaction = transaction;

                var asset = user.UserAssets.Find(w => w.StockId == stockId);

                if (asset.Id == 0)
                {
                    cmd.CommandText = "INSERT INTO UserAsset (Quantity, UserId, StockId) VALUES (@quantity, @userId, @stockId)";
                    cmd.Parameters.AddWithValue("@quantity", asset.Quantity);
                    cmd.Parameters.AddWithValue("@userId", user.Id);
                    cmd.Parameters.AddWithValue("@stockId", asset.StockId);
                }
                else
                {
                    cmd.CommandText = "UPDATE UserAsset SET Quantity = @quantity WHERE Id = @assetId";
                    cmd.Parameters.AddWithValue("@quantity", asset.Quantity);
                    cmd.Parameters.AddWithValue("@assetId", asset.Id);
                }

                await cmd.ExecuteNonQueryAsync();

                cmd.CommandText = "UPDATE User SET Balance = @balance WHERE Id = @id";
                cmd.Parameters.AddWithValue("@balance", user.Balance);
                cmd.Parameters.AddWithValue("@Id", user.Id);

                await cmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            }
            catch (MySqlException)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
