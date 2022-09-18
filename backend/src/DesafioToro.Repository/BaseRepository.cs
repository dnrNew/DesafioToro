using MySqlConnector;

namespace DesafioToro.Repository
{
    public class BaseRepository : IDisposable
    {
        protected MySqlConnection _connection;

        public BaseRepository(MySqlConnection connection)
        {
            _connection = connection;
            _connection.Open();
        }

        public void Dispose()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }
    }
}
