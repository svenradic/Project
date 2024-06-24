using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Stocks.Common;
using Stocks.Model;
using Stocks.REST_Models;
using Stocks.Service.Common;
using Stocks.WebAPI;

namespace Stocks.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class TraderController : ControllerBase
    {
        private IService<Trader> _traderService;
        private IMapper _traderMapper;
        public TraderController(IService<Trader> traderService, IMapper traderMapper)
        {
            this._traderService = traderService;
            this._traderMapper = traderMapper;
        }

        [HttpGet("traders")]
        public async Task<IActionResult> Get(Guid? traderId = null, string name = "", DateTime? minDateTime = null,
            DateTime? maxDateTime = null, string orderBy = "Name", string sortOrder = "ASC", int rpp = 10, int pageNumber = 1)
        {
            try
            {
                IFilter filter = new TraderFilter(traderId, name, minDateTime, maxDateTime);
                SortingParameters order = new SortingParameters(orderBy, sortOrder);
                PageFilter page = new PageFilter(rpp, pageNumber);

                ICollection<Trader> traders = await _traderService.GetAsync(filter, order, page);
                ICollection<TraderGetRest> tradersRest = new List<TraderGetRest>();
                foreach (Trader trader in traders)
                {
                    TraderGetRest traderRest = _traderMapper.Map<TraderGetRest>(trader);
                    tradersRest.Add(traderRest);
                }
                return Ok(tradersRest);
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
                Trader? trader = await _traderService.GetAsync(traderId);
                if(trader == null)
                {
                    return NotFound();
                }
                return Ok(trader);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }


}

