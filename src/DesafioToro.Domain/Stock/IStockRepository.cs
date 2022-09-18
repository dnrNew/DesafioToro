﻿namespace DesafioToro.Domain.Stock
{
    public interface IStockRepository : IDisposable
    {
        Task<List<Stock>> GetStocks();
    }
}
