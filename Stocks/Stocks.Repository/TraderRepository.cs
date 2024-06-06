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
    public class TraderRepository : IRepository<Trader>
    {
        private readonly string connectionString;

        public TraderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private NpgsqlCommand CreateCommand(NpgsqlConnection connection, IFilter filter, OrderByFilter order, PageFilter page)
        {
            NpgsqlCommand command = new NpgsqlCommand("", connection);
            StringBuilder query = new StringBuilder();
            query.Append("SELECT  t.\"Id\" , t.\"Name\" , t.\"DateOfBirth\", s.\"Id\", s.\"Symbol\", s.\"CompanyName\", s.\"CurrentPrice\", s.\"MarketCap\", s.\"TraderId\" FROM \"Trader\" t" +
                " LEFT JOIN \"Stock\" s ON t.\"Id\" = s.\"TraderId\"" +
                " WHERE t.\"IsActive\" = @isActive AND s.\"IsActive\" = @isActive");

            command.Parameters.AddWithValue("@IsActive", true);
            if (filter.Id !=  null)
            {
                query.Append(" AND t.\"Id\" = @Id");
                command.Parameters.AddWithValue("@Id", filter.Id);
            }
            if (filter.Name != "")
            {
                query.Append(" AND t.\"Name\" ILIKE @Name");
                command.Parameters.AddWithValue("@Name", $"%{filter.Name}%");
            }
            if (filter.MinDateOfBirth != null)
            {
                query.Append(" AND t.\"DateOfBirth\" >= @DateOfBirth");
                command.Parameters.AddWithValue("@DateOfBirth", filter.MinDateOfBirth);
            }
            if (filter.MaxDateOfBirth != null)
            {
                query.Append(" AND t.\"DateOfBirth\" >= @MaxDateOfBirth");
                command.Parameters.AddWithValue("@MaxDateOfBirth", filter.MaxDateOfBirth);
            }

            string sortOrder = order.sortOrder.ToUpper() == "ASC" ? "ASC" : "DESC";
            query.Append($" ORDER BY t.\"{order.orderBy}\" {sortOrder}");

            query.Append(" LIMIT @Limit OFFSET @Offset");
            command.Parameters.AddWithValue("@Limit", page.rpp);
            command.Parameters.AddWithValue("@Offset", (page.pageNumber - 1) * page.rpp);
            command.CommandText = query.ToString();
            return command;
        }
        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> PostAsync(Stock stock)
        {
            throw new NotImplementedException();
        }

        public Task<int> PutAsync(Stock stock, Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Trader>> GetAsync(IFilter filter, OrderByFilter order, PageFilter page)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = CreateCommand(connection, filter, order, page);
            ICollection<Trader> traders = new List<Trader>();
            connection.Open();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var traderId = reader.GetGuid(0);
                    Trader? existingTrader = traders.FirstOrDefault(t => t.Id == traderId);

                    if (existingTrader == null)
                    {
                        var trader = new Trader
                        {
                            Id = traderId,
                            Name = reader.GetString(1),
                            DateOfBirth = reader.GetDateTime(2),
                            Stocks = new List<Stock>()
                        };

                        if (!reader.IsDBNull(3))
                        {
                            var stock = new Stock
                            {
                                Id = reader.GetGuid(3),
                                Symbol = reader.GetString(4),
                                CompanyName = reader.GetString(5),
                                CurrentPrice = reader.GetDouble(6),
                                MarketCap = (long)reader.GetDouble(7),
                                TraderId = reader.GetGuid(8)
                            };

                            trader.Stocks.Add(stock);
                        }

                        traders.Add(trader);
                    }
                    else if (!reader.IsDBNull(3))
                    {
                        var stock = new Stock
                        {
                            Id = reader.GetGuid(3),
                            Symbol = reader.GetString(4),
                            CompanyName = reader.GetString(5),
                            CurrentPrice = reader.GetDouble(6),
                            MarketCap = (long)reader.GetDouble(7),
                            TraderId = reader.GetGuid(8)
                        };

                        existingTrader.Stocks.Add(stock);
                    }
                }
            }
            connection.Close();

            return traders;
        }
    }
    
}
