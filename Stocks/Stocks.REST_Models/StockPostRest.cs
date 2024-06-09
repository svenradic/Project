using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.REST_Models
{
    public class StockPostRest
    {
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public double? CurrentPrice { get; set; }
        public long? MarketCap { get; set; }
        public Guid? TraderId { get; set; }
 
    }
}
