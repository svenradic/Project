using Microsoft.AspNetCore.Mvc;
using Npgsql;


namespace Stocks.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class StockController: ControllerBase
    {

        private IConfiguration configuration;
        public StockController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("stocks")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                using NpgsqlConnection conn = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
                using NpgsqlCommand command = new NpgsqlCommand("", conn);
                ICollection<Stock> stocks = new List<Stock>();
                await conn.OpenAsync();

                command.CommandText = "SELECT * FROM \"Stocks\"";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var isActive = reader.GetBoolean(6);
                        
                        if (isActive == true)
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
                }

                return Ok(stocks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("stocks/{stockId:guid}")]
        public async Task<IActionResult> Get(Guid stockId)
        {
            try
            {
                using NpgsqlConnection conn = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
                using NpgsqlCommand command = new NpgsqlCommand("", conn);
                Stock stock = new Stock();
                await conn.OpenAsync();

                command.CommandText = "SELECT * FROM \"Stocks\" WHERE \"Stocks\".\"Id\" = @Id";
                command.Parameters.AddWithValue("@Id", stockId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    { 
                        if(reader.GetBoolean(6) == true)
                        {
                            stock.Id = reader.GetGuid(0);
                            stock.Symbol = reader.GetString(1);
                            stock.CompanyName = reader.GetString(2);
                            stock.CurrentPrice = reader.GetDouble(3);
                            stock.MarketCap = (long)reader.GetDouble(4);
                            stock.TraderId = reader.GetGuid(5);
                        }

                    }
                }

                return Ok(stock);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }



        [HttpPost("stocks")]
        public async Task<IActionResult> Post(Stock stock)
        {
            if(stock == null)
            {
                return BadRequest();
            }
            try
            {
                using NpgsqlConnection conn = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
                using NpgsqlCommand comand = new NpgsqlCommand("", conn);
                await conn.OpenAsync(); 

                comand.CommandText = "INSERT INTO \"Stocks\" (\"Symbol\", \"CompanyName\", \"CurrentPrice\", \"MarketCap\", \"TraderId\") " +
                    "VALUES (@Symbol, @CompanyName, @CurrentPrice, @MarketCap, @TraderId)";
                comand.Parameters.AddWithValue("@Symbol", stock.Symbol);
                comand.Parameters.AddWithValue("@CompanyName", stock.CompanyName);
                comand.Parameters.AddWithValue("@CurrentPrice", stock.CurrentPrice);
                comand.Parameters.AddWithValue("@MarketCap", stock.MarketCap);
                comand.Parameters.AddWithValue("@TraderId", stock.TraderId);
                
                


                int commitNumber = await comand.ExecuteNonQueryAsync();
                if(commitNumber == 0)
                {
                    return BadRequest();
                }
                return Ok("Stock added successfully.");
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPut("stocks/{stockId:guid}")]
        public async Task<IActionResult> Put(Stock stock, Guid stockId)
        {
            if (stock == null)
            {
                return BadRequest();
            }
            try
            {
                using NpgsqlConnection conn = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
                using NpgsqlCommand comand = new NpgsqlCommand("", conn);
                await conn.OpenAsync();

                comand.CommandText = "UPDATE \"Stocks\" SET \"Symbol\" = @Symbol, \"CompanyName\" = @CompanyName, " +
                    "\"CurrentPrice\" = @CurrentPrice, \"MarketCap\" = @MarketCap, \"TraderId\" = @TraderId WHERE \"Id\" = @stockId";

                comand.Parameters.AddWithValue("@Symbol", stock.Symbol);
                comand.Parameters.AddWithValue("@CompanyName", stock.CompanyName);
                comand.Parameters.AddWithValue("@CurrentPrice", stock.CurrentPrice);
                comand.Parameters.AddWithValue("@MarketCap", stock.MarketCap);
                comand.Parameters.AddWithValue("@TraderId", stock.TraderId);
                comand.Parameters.AddWithValue("@stockId", stockId);


                int commitNumber = await comand.ExecuteNonQueryAsync();
                if (commitNumber == 0)
                {
                    return BadRequest();
                }
                return Ok("Stock updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }



        [HttpDelete("stocks/{stockId:guid}")]
        public async Task<IActionResult> Delete(Guid stockId)
        {
            try
            {
                using NpgsqlConnection conn = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
                using NpgsqlCommand comand = new NpgsqlCommand("", conn);
                await conn.OpenAsync();

                comand.CommandText = "UPDATE \"Stocks\" SET \"IsActive\" = @IsActive WHERE \"Id\" = @stockId";

                comand.Parameters.AddWithValue("@IsActive", false);
                comand.Parameters.AddWithValue("@stockId", stockId);


                int commitNumber = await comand.ExecuteNonQueryAsync();
                if (commitNumber == 0)
                {
                    return BadRequest();
                }
                return Ok("Stock deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}
