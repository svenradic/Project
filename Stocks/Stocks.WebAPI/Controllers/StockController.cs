using Microsoft.AspNetCore.Mvc;

namespace Stocks.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class StockController: ControllerBase
    {
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

        [HttpPost("stocks")]
        public IActionResult Post(Stock stock) 
        {
            if(stock == null)
            {
                return BadRequest();
            }
            var addedStock = StockRepository.Add(stock);
            return Ok(addedStock);
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
