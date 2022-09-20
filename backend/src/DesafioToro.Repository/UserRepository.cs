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

            using var command = new MySqlCommand($@"SELECT u.Id, u.Name, u.Cpf, u.Account, u.Balance, ua.Id, ua.Quantity, s.Id, s.Symbol 
                                                    FROM User u 
                                                    JOIN UserAsset ua ON ua.UserId = u.Id
                                                    JOIN Stock s ON s.Id = ua.StockId 
                                                    WHERE u.Id = {userId};", _connection);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                if (user.Id == 0)
                {
                    user.Id = reader.GetInt16(0);
                    user.Name = reader.GetString(1);
                    user.Cpf = reader.GetString(2);
                    user.Account = reader.GetString(3);
                    user.Balance = reader.GetDecimal(4);
                }

                user.UserAssets.Add(
                    new UserAsset()
                    {
                        Id = reader.GetInt16(5),
                        Quantity = reader.GetInt16(6),
                        StockId = reader.GetInt16(7),
                        Stock = new Stock() { 
                            Id = reader.GetInt16(7),
                            Symbol = reader.GetString(8) 
                        }
                    });
            }

            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = new List<User>();

            using var command = new MySqlCommand($"SELECT Id, Name, Cpf, Account, Balance FROM User;", _connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var user = new User();

                user.Id = reader.GetInt16(0);
                user.Name = reader.GetString(1);
                user.Cpf = reader.GetString(2);
                user.Account = reader.GetString(3);
                user.Balance = reader.GetDecimal(4);

                users.Add(user);
            }

            return users;
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
