using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Stocks.Model;
using Stocks.Service;
using Stocks.WebAPI;

namespace Stocks.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class TraderController : ControllerBase
    {
        private TraderService traderService;
        public TraderController(IConfiguration configuration)
        {
            this.traderService = new TraderService(configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet("traders")]
        public async Task<IActionResult> GetAll()
        {
            // var conn = WebApplication.Create().Configuration.GetConnectionString("DefaultConnection");
            try
            {
                ICollection<Trader> traders = await traderService.GetAllAsync();
                return Ok(traders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("traders/{traderId:guid}")]
        public async Task<IActionResult> Get(Guid traderId)
        {
            try
            {
                Trader trader = await traderService.GetAsync(traderId);

                return Ok(trader);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}
