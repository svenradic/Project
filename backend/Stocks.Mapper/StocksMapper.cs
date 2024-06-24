using AutoMapper;
using Stocks.Model;
using Stocks.REST_Models;
using Stocks.WebAPI.RESTModels;

namespace Stocks.Mapper
{
    public class StocksMapper: Profile
    {
        public StocksMapper()
        {
            CreateMap<Stock, StockGetRest>();
            CreateMap<StockPostRest, Stock>();
            CreateMap<StockPutRest, Stock>();
            CreateMap<Trader, TraderGetRest>();
        }
    }
}
