using DesafioToro.Domain.User;
using MySqlConnector;

namespace DesafioToro.Repository
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private MySqlConnection _connection;

        public UserRepository(MySqlConnection connection)
        {
            _connection = connection;
            _connection.Open();
        }

        public void Dispose()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
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
    }
}
