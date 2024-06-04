using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Stocks.Model;
using Stocks.Repository.Common;

namespace Stocks.Repository
{
    public class StockRepository : IRepository<Stock>
    {
        private readonly string connectionString;

        public StockRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<int> Delete(Guid id)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand comand = new NpgsqlCommand("", conn);
            await conn.OpenAsync();

            comand.CommandText = "UPDATE \"Stock\" SET \"IsActive\" = @IsActive WHERE \"Id\" = @stockId";

            comand.Parameters.AddWithValue("@IsActive", false);
            comand.Parameters.AddWithValue("@stockId", id);


            int commitNumber = await comand.ExecuteNonQueryAsync();
            return commitNumber;
        }

        public async Task<Stock> Get(Guid id)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            Stock stock = new Stock();
            await conn.OpenAsync();

            command.CommandText = "SELECT * FROM \"Stock\" WHERE \"Stock\".\"Id\" = @Id and \"IsActive\" = @isActive";
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@isActive", true);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    stock.Id = reader.GetGuid(0);
                    stock.Symbol = reader.GetString(1);
                    stock.CompanyName = reader.GetString(2);
                    stock.CurrentPrice = reader.GetDouble(3);
                    stock.MarketCap = (long)reader.GetDouble(4);
                    stock.TraderId = reader.GetGuid(5);
                }
            }

            return stock;
        }

        public async Task<ICollection<Stock>> GetAll()
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            ICollection<Stock> stocks = new List<Stock>();
            await conn.OpenAsync();

            command.CommandText = "SELECT * FROM \"Stock\" WHERE \"IsActive\" = @isActive";
            command.Parameters.AddWithValue("@isActive", true);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var stock = new Stock
                    {
                        Id = reader.GetGuid(0),
                        Symbol = reader.GetString(1),
                        CompanyName = reader.GetString(2),
                        CurrentPrice = reader.GetDouble(3),
                        MarketCap = (long)reader.GetDouble(4),
                        TraderId = reader.GetGuid(5)
                    };
                    stocks.Add(stock);

                }
            }
            return stocks;
        }

        public async Task<int> Post(Stock stock)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand comand = new NpgsqlCommand("", conn);
            await conn.OpenAsync();

            comand.CommandText = "INSERT INTO \"Stock\" (\"Symbol\", \"CompanyName\", \"CurrentPrice\", \"MarketCap\", \"TraderId\") " +
                "VALUES (@Symbol, @CompanyName, @CurrentPrice, @MarketCap, @TraderId)";
            comand.Parameters.AddWithValue("@Symbol", stock.Symbol);
            comand.Parameters.AddWithValue("@CompanyName", stock.CompanyName);
            comand.Parameters.AddWithValue("@CurrentPrice", stock.CurrentPrice);
            comand.Parameters.AddWithValue("@MarketCap", stock.MarketCap);
            comand.Parameters.AddWithValue("@TraderId", stock.TraderId);

            int commitNumber = await comand.ExecuteNonQueryAsync();
            return commitNumber;
        }

        public async Task<int> Put(Stock stock, Guid id)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand comand = new NpgsqlCommand("", conn);
            await conn.OpenAsync();

            comand.CommandText = "UPDATE \"Stock\" SET \"Symbol\" = @Symbol, \"CompanyName\" = @CompanyName, " +
                "\"CurrentPrice\" = @CurrentPrice, \"MarketCap\" = @MarketCap, \"TraderId\" = @TraderId WHERE \"Id\" = @stockId";

            comand.Parameters.AddWithValue("@Symbol", stock.Symbol);
            comand.Parameters.AddWithValue("@CompanyName", stock.CompanyName);
            comand.Parameters.AddWithValue("@CurrentPrice", stock.CurrentPrice);
            comand.Parameters.AddWithValue("@MarketCap", stock.MarketCap);
            comand.Parameters.AddWithValue("@TraderId", stock.TraderId);
            comand.Parameters.AddWithValue("@stockId", id);


            int commitNumber = await comand.ExecuteNonQueryAsync();
            return commitNumber;
        }
    }
}
