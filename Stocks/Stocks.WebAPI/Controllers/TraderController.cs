using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Stocks.Common;
using Stocks.Model;
using Stocks.Service.Common;
using Stocks.WebAPI;

namespace Stocks.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class TraderController : ControllerBase
    {
        private IService<Trader> traderService;
        public TraderController(IService<Trader> traderService)
        {
            this.traderService = traderService;
        }

        [HttpGet("traders")]
        public async Task<IActionResult> Get(Guid? traderId = null, string name = "", DateTime? minDateTime = null,
            DateTime? maxDateTime = null, string orderBy = "Name", string sortOrder = "ASC", int rpp = 10, int pageNumber = 1)
        {
            try
            {
                IFilter filter = new TraderFilter(traderId, name, minDateTime, maxDateTime);
                OrderByFilter order = new OrderByFilter(orderBy, sortOrder);
                PageFilter page = new PageFilter(rpp, pageNumber);

                ICollection<Trader> traders = await traderService.GetAsync(filter, order, page);
                return Ok(traders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

      
    }
}
