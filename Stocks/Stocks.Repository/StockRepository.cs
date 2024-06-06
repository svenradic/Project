using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        private NpgsqlCommand CreateCommandSelect(NpgsqlConnection connection, IFilter filter, OrderByFilter order, PageFilter page)
        {
            NpgsqlCommand command = new NpgsqlCommand("", connection);
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM \"Stock\" WHERE \"IsActive\" = @IsActive");
            
            command.Parameters.AddWithValue("@IsActive", true);
            if (filter.Id != null)
            {
                query.Append(" AND \"Id\" = @Id");
                command.Parameters.AddWithValue("@Id", filter.Id);
            }
            if (filter.SymbolQuery != "")
            {
                query.Append(" AND \"Symbol\" ILIKE @Symbol");
                command.Parameters.AddWithValue("@Symbol", $"%{filter.SymbolQuery}%");
            }
            if(filter.CompanyQuery != "")
            {
                query.Append(" AND \"CompanyName\" ILIKE @CompanyName");
                command.Parameters.AddWithValue("@CompanyName", $"%{filter.CompanyQuery}%");
            }
            if (filter.MinMarketCap != null)
            {
                query.Append(" AND \"MarketCap\" >= @MinMarketCap");
                command.Parameters.AddWithValue("@MinMarketCap", filter.MinMarketCap);
            }
            if (filter.MaxMarketCap != null)
            {
                query.Append(" AND \"MarketCap\" <= @MaxMarketCap");
                command.Parameters.AddWithValue("@MaxMarketCap", filter.MaxMarketCap);
            }
            if(filter.MinCurrentPrice != null)
            {
                query.Append(" AND \"CurrentPrice\" >= @MinCurrentPrice");
                command.Parameters.AddWithValue("@MinCurrentPrice", filter.MinCurrentPrice);
            }
            if(filter.MaxCurrentPrice != null)
            {
                query.Append(" AND \"CurrentPrice\" <= @MaxCurrentPrice");
                command.Parameters.AddWithValue("@MaxCurrentPrice", filter.MaxCurrentPrice);
            }

            string sortOrder = order.sortOrder.ToUpper() == "ASC" ? "ASC" : "DESC";
            query.Append($" ORDER BY \"{order.orderBy}\" {sortOrder}");
            
            query.Append(" LIMIT @Limit OFFSET @Offset");
            command.Parameters.AddWithValue("@Limit", page.rpp);
            command.Parameters.AddWithValue("@Offset", (page.pageNumber-1) * page.rpp);
            command.CommandText = query.ToString();
            return command;
        }

        private NpgsqlCommand CreateCommandPost(Stock stock, NpgsqlConnection connection)
        {
            NpgsqlCommand command = new NpgsqlCommand("", connection);
            StringBuilder query = new StringBuilder();

            query.Append("INSERT INTO \"Stock\" (\"Symbol\", \"CompanyName\", \"CurrentPrice\", \"MarketCap\"");
            if (stock.TraderId != null)
            {
                query.Append(", \"TraderId\"");
                command.Parameters.AddWithValue("@TraderId", stock.TraderId);
            }
            else
            {
                query.Append(") VALUES (@Symbol, @CompanyName, @CurrentPrice, @MarketCap)");
            }
            command.Parameters.AddWithValue("@Symbol", stock.Symbol);
            command.Parameters.AddWithValue("@CompanyName", stock.CompanyName);
            command.Parameters.AddWithValue("@CurrentPrice", stock.CurrentPrice);
            command.Parameters.AddWithValue("@MarketCap", stock.MarketCap);

            command.CommandText = query.ToString();
            return command;
        }


        public async Task<int> DeleteAsync(Guid id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            using NpgsqlCommand comand = new NpgsqlCommand("", connection);
            connection.Open();

            comand.CommandText = "UPDATE \"Stock\" SET \"IsActive\" = @IsActive WHERE \"Id\" = @stockId";

            comand.Parameters.AddWithValue("@IsActive", false);
            comand.Parameters.AddWithValue("@stockId", id);

            int commitNumber = await comand.ExecuteNonQueryAsync();
            connection.Close();
            return commitNumber;
        }

       
        public async Task<ICollection<Stock>> GetAsync(IFilter filter, OrderByFilter order, PageFilter page)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = CreateCommandSelect(connection, filter, order, page);
            ICollection<Stock> stocks = new List<Stock>();
            connection.Open();

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
                        TraderId = reader.IsDBNull(5) ? null : reader.GetGuid(5),
                    };
                    stocks.Add(stock);  
                }
            }
            connection.Close();
            return stocks;
        }


        public async Task<int> PostAsync(Stock stock)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            using NpgsqlCommand comand = CreateCommandPost(stock, connection);
            connection.Open();

            int commitNumber = await comand.ExecuteNonQueryAsync();

            connection.Close();
            return commitNumber;
        }

        public async Task<int> PutAsync(Stock stock, Guid id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            using NpgsqlCommand comand = new NpgsqlCommand("", connection);
            connection.Open();

            comand.CommandText = "UPDATE \"Stock\" SET \"Symbol\" = @Symbol, \"CompanyName\" = @CompanyName, " +
                "\"CurrentPrice\" = @CurrentPrice, \"MarketCap\" = @MarketCap, \"TraderId\" = @TraderId WHERE \"Id\" = @stockId";

            comand.Parameters.AddWithValue("@Symbol", stock.Symbol);
            comand.Parameters.AddWithValue("@CompanyName", stock.CompanyName);
            comand.Parameters.AddWithValue("@CurrentPrice", stock.CurrentPrice);
            comand.Parameters.AddWithValue("@MarketCap", stock.MarketCap);
            comand.Parameters.AddWithValue("@TraderId", stock.TraderId is null ? DBNull.Value : stock.TraderId);
            comand.Parameters.AddWithValue("@stockId", id);

            int commitNumber = await comand.ExecuteNonQueryAsync();
            connection.Close();
            return commitNumber;
        }
    }
}
