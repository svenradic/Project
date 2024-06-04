using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Stocks.Model;
using Stocks.Service;



namespace Stocks.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class StockController: ControllerBase
    {
        private StockService stockService;
        public StockController(IConfiguration configuration)
        {
            this.stockService = new StockService(configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet("stocks")]
        public async Task<IActionResult> GetAll()
        {
            // var conn = WebApplication.Create().Configuration.GetConnectionString("DefaultConnection");
            try
            {
                ICollection<Stock> stocks = await stockService.GetAll();

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
                Stock stock = await stockService.Get(stockId);

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
                int commitNumber = await stockService.Post(stock);

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
            try{
                int commitNumber = await stockService.Put(stock, stockId);
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
                int commitNumber = await stockService.Delete(stockId);
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
