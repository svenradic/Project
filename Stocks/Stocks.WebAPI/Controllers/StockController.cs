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
        public IActionResult Get()
        {
            List<Stock> stocks = StockRepository.GetAll().ToList();
            if(stocks == null)
            {
                return BadRequest();
            }
            return Ok(stocks);
        }

        [HttpGet("stocks/{stockId:int}")]
        public IActionResult GetStock(int stockId)
        {
            Stock? stock = StockRepository.Get(stockId);
            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }
        /*
        [HttpPost("stocks")]
        public IActionResult Post(Stock stock) 
        {
            if(stock == null)
            {
                return BadRequest();
            }
            var addedStock = StockRepository.Add(stock);
            return Ok(addedStock);
        }*/

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

                comand.CommandText = "INSERT INTO \"Stocks\" (\"Symbol\", \"CompanyName\", \"CurrentPrice\", \"MarketCap\") " +
                    "VALUES (@Symbol, @CompanyName, @CurrentPrice, @MarketCap)";
                comand.Parameters.AddWithValue("@Symbol", stock.Symbol);
                comand.Parameters.AddWithValue("@CompanyName", stock.CompanyName);
                comand.Parameters.AddWithValue("@CurrentPrice", stock.CurrentPrice);
                comand.Parameters.AddWithValue("@MarketCap", stock.MarketCap);

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



        [HttpPut("stocks/{stockId:int}")]
        public IActionResult Put(Stock stock, int stockId)
        {
            Stock? updatedStock = StockRepository.Update(stock, stockId);
            if (updatedStock == null)
            {
                return BadRequest();
            }
            else return Ok(updatedStock);
        }
        [HttpDelete("stocks/{stockId:int}")]
        public IActionResult Delete(int stockId) 
        {
            int check = StockRepository.Delete(stockId);
            if (check == 1) return Ok();
            else return BadRequest();
        }
    }
}
