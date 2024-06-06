using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Stocks.Common;
using Stocks.Model;
using Stocks.Service.Common;



namespace Stocks.WebAPI.Controllersyy
{
    [ApiController]
    [Route("[controller]/")]
    public class StockController: ControllerBase
    {
        private IService<Stock> stockService;
        public StockController(IService<Stock> stockService)
        {
            this.stockService = stockService;
        }

        [HttpGet("stocks")]
        public async Task<IActionResult> Get(Guid? stockId = null, string symbolQuery = "", string companyQuery = "", 
            long? minMarketCap = null, long? maxMarketCap = null, double? minCurrentPrice = null, double? maxCurrentPrice = null,
            string orderBy = "Symbol", string sortOrder = "ASC", int rpp = 10, int pageNumber = 1)
        {
            try
            {
                IFilter filter = new StockFilter(stockId, symbolQuery, companyQuery, minMarketCap, maxMarketCap, minCurrentPrice, maxCurrentPrice);
                OrderByFilter order = new OrderByFilter(orderBy, sortOrder);
                PageFilter page = new PageFilter(rpp, pageNumber);

                ICollection<Stock> stocks = await stockService.GetAsync(filter, order, page);

                return Ok(stocks);
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
                int commitNumber = await stockService.PostAsync(stock);

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
                int commitNumber = await stockService.PutAsync(stock, stockId);
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
                int commitNumber = await stockService.DeleteAsync(stockId);
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
