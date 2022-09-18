﻿using DesafioToro.Domain.Stocks;
using MySqlConnector;

namespace DesafioToro.Repository
{
    public class StockRepository : BaseRepository, IStockRepository
    {
        public StockRepository(MySqlConnection connection) : base(connection)
        {
        }

        public async Task<List<Stock>> GetStocks()
        {
            var stocks = new List<Stock>();

            using var command = new MySqlCommand("SELECT * FROM Stock;", _connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var stock = new Stock();

                stock.Id = reader.GetInt16(0);
                stock.Symbol = reader.GetString(1);
                stock.CurrentPrice = reader.GetDecimal(2);

                stocks.Add(stock);
            }

            return stocks;
        }

        public async Task<Stock> GetStockBySymbol(string symbol)
        {
            var stock = new Stock();

            using var command = new MySqlCommand("SELECT * FROM Stock WHERE Symbol = @symbol;", _connection);
            command.Parameters.AddWithValue("@symbol", symbol);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                stock.Id = reader.GetInt16(0);
                stock.Symbol = reader.GetString(1);
                stock.CurrentPrice = reader.GetDecimal(2);
            }

            return stock;
        }
    }
}
