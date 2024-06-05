using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Stocks.Common;
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

        public async Task<int> DeleteAsync(Guid id)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand comand = new NpgsqlCommand("", conn);
            conn.Open();

            comand.CommandText = "UPDATE \"Stock\" SET \"IsActive\" = @IsActive WHERE \"Id\" = @stockId";

            comand.Parameters.AddWithValue("@IsActive", false);
            comand.Parameters.AddWithValue("@stockId", id);

            int commitNumber = await comand.ExecuteNonQueryAsync();
            conn.Close();
            return commitNumber;
        }

        public async Task<Stock> GetAsync(Guid id)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            Stock stock = new Stock();
            conn.Open();

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
            conn.Close();
            return stock;
        }
        private static NpgsqlCommand CreateCommand(NpgsqlConnection conn, StockFilter filter, OrderByFilter order, PageFilter page)
        {
            NpgsqlCommand command = new NpgsqlCommand("", conn);
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM \"Stock\" WHERE \"IsActive\" = @IsActive");
            
            command.Parameters.AddWithValue("@IsActive", true);
            if (filter.id != null)
            {
                query.Append(" AND \"Id\" = @Id");
                command.Parameters.AddWithValue("@Id", filter.id);
            }
            if (filter.symbolQuery != "")
            {
                query.Append(" AND \"Symbol\" ILIKE @Symbol");
                command.Parameters.AddWithValue("@Symbol", $"%{filter.symbolQuery}%");
            }
            if(filter.companyQuery != "")
            {
                query.Append(" AND \"CompanyName\" ILIKE @CompanyName");
                command.Parameters.AddWithValue("@CompanyName", $"%{filter.companyQuery}%");
            }
            if (filter.minMarketCap != null)
            {
                query.Append(" AND \"MarketCap\" >= @MinMarketCap");
                command.Parameters.AddWithValue("@MinMarketCap", filter.minMarketCap);
            }
            if (filter.maxMarketCap != null)
            {
                query.Append(" AND \"MarketCap\" <= @MaxMarketCap");
                command.Parameters.AddWithValue("@MaxMarketCap", filter.maxMarketCap);
            }
            if(filter.minCurrentPrice != null)
            {
                query.Append(" AND \"CurrentPrice\" >= @MinCurrentPrice");
                command.Parameters.AddWithValue("@MinCurrentPrice", filter.minCurrentPrice);
            }
            if(filter.maxCurrentPrice != null)
            {
                query.Append(" AND \"CurrentPrice\" <= @MaxCurrentPrice");
                command.Parameters.AddWithValue("@MaxCurrentPrice", filter.maxCurrentPrice);
            }

            string sortOrder = order.sortOrder.ToUpper() == "DESC" ? "DESC" : "ASC";
            query.Append($" ORDER BY \"{order.orderBy}\" {sortOrder}");
            
            query.Append(" LIMIT @Limit OFFSET @Offset");
            command.Parameters.AddWithValue("@Limit", page.rpp);
            command.Parameters.AddWithValue("@Offset", (page.pageNumber-1) * page.rpp);
            command.CommandText = query.ToString();
            return command;
        }
        public async Task<ICollection<Stock>> GetAsync(StockFilter filter, OrderByFilter order, PageFilter page)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = CreateCommand(conn, filter, order, page);
            ICollection<Stock> stocks = new List<Stock>();
            conn.Open();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Stock stock = new Stock()
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
            conn.Close();
            return stocks;
        }

        public async Task<ICollection<Stock>> GetAllAsync()
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            ICollection<Stock> stocks = new List<Stock>();
            conn.Open();

            command.CommandText = "SELECT * FROM \"Stock\" WHERE \"IsActive\" = @isActive" ;
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
            conn.Close();
            return stocks;
        }

        public async Task<int> PostAsync(Stock stock)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand comand = new NpgsqlCommand("", conn);
            conn.Open();

            comand.CommandText = "INSERT INTO \"Stock\" (\"Symbol\", \"CompanyName\", \"CurrentPrice\", \"MarketCap\", \"TraderId\") " +
                "VALUES (@Symbol, @CompanyName, @CurrentPrice, @MarketCap, @TraderId)";
            comand.Parameters.AddWithValue("@Symbol", stock.Symbol);
            comand.Parameters.AddWithValue("@CompanyName", stock.CompanyName);
            comand.Parameters.AddWithValue("@CurrentPrice", stock.CurrentPrice);
            comand.Parameters.AddWithValue("@MarketCap", stock.MarketCap);
            comand.Parameters.AddWithValue("@TraderId", stock.TraderId);

            int commitNumber = await comand.ExecuteNonQueryAsync();

            conn.Close();
            return commitNumber;
        }

        public async Task<int> PutAsync(Stock stock, Guid id)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand comand = new NpgsqlCommand("", conn);
            conn.Open();

            comand.CommandText = "UPDATE \"Stock\" SET \"Symbol\" = @Symbol, \"CompanyName\" = @CompanyName, " +
                "\"CurrentPrice\" = @CurrentPrice, \"MarketCap\" = @MarketCap, \"TraderId\" = @TraderId WHERE \"Id\" = @stockId";

            comand.Parameters.AddWithValue("@Symbol", stock.Symbol);
            comand.Parameters.AddWithValue("@CompanyName", stock.CompanyName);
            comand.Parameters.AddWithValue("@CurrentPrice", stock.CurrentPrice);
            comand.Parameters.AddWithValue("@MarketCap", stock.MarketCap);
            comand.Parameters.AddWithValue("@TraderId", stock.TraderId);
            comand.Parameters.AddWithValue("@stockId", id);

            int commitNumber = await comand.ExecuteNonQueryAsync();
            conn.Close();
            return commitNumber;
        }
    }
}
