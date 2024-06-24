using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Stocks.Common;
using Stocks.Model;
using Stocks.REST_Models;
using Stocks.Service.Common;
using Stocks.WebAPI.RESTModels;



namespace Stocks.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class StockController: ControllerBase
    {
        private IService<Stock> _stockService;
        private IService<Trader> _traderService;
        private IMapper _stockMapper;
        public StockController(IService<Stock> stockService, IService<Trader> traderService, IMapper stockMapper)
        {
            this._stockService = stockService;
            this._traderService = traderService;
            this._stockMapper = stockMapper;
        }

        [HttpGet("stocks")]
        public async Task<IActionResult> Get(Guid? stockId = null, string symbolQuery = "", string companyQuery = "", 
            long? minMarketCap = null, long? maxMarketCap = null, double? minCurrentPrice = null, double? maxCurrentPrice = null,
            string orderBy = "Symbol", string sortOrder = "ASC", int rpp = 10, int pageNumber = 1)
        {
            try
            {
                IFilter filter = new StockFilter(stockId, symbolQuery, companyQuery, minMarketCap, maxMarketCap, minCurrentPrice, maxCurrentPrice);
                SortingParameters order = new SortingParameters(orderBy, sortOrder);
                PageFilter page = new PageFilter(rpp, pageNumber);

                ICollection<Stock> stocks = await _stockService.GetAsync(filter, order, page);

                ICollection<StockGetRest> stocksRest = new List<StockGetRest>();
                foreach (Stock stock in stocks)
                {
                    StockGetRest stockRest = _stockMapper.Map<StockGetRest>(stock);
                    stocksRest.Add(stockRest);

                }
                return Ok(stocksRest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost("stocks")]
        public async Task<IActionResult> Post(StockPostRest stockPost)
        {
            if(stockPost == null)
            {
                return BadRequest();
            }
            if(stockPost.TraderId != null)
            {
                Trader? existingTrader = await _traderService.GetAsync(stockPost.TraderId);
                if(existingTrader == null) 
                {
                    return NotFound();
                }
            }
            try
            {
                Stock stock = _stockMapper.Map<Stock>(stockPost);
                int commitNumber = await _stockService.PostAsync(stock);

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
        public async Task<IActionResult> Put(StockPostRest stockPost, Guid stockId)
        {
            if (stockPost == null)
            {
                return BadRequest();
            }
            try{
                Stock stock = _stockMapper.Map<Stock>(stockPost);
                
                int commitNumber = await _stockService.PutAsync(stock, stockId);
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
                int commitNumber = await _stockService.DeleteAsync(stockId);
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
