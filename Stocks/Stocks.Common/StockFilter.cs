using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Common
{
    public class StockFilter
    {
        public Guid? id;
        public string symbolQuery;
        public string companyQuery;
        public long? maxMarketCap;
        public long? minMarketCap;
        public double? minCurrentPrice;
        public double? maxCurrentPrice; 

        public StockFilter(Guid? id, string symbolQuery, string companyQuery, long? minMarketCap, long? maxMarketCap, 
            double? minCurrentPrice, double? maxCurrentPrice)
        {
            this.id = id;
            this.symbolQuery = symbolQuery;
            this.companyQuery = companyQuery;
            this.maxMarketCap = maxMarketCap;
            this.minMarketCap = minMarketCap;
            this.minCurrentPrice = minCurrentPrice;
            this.maxCurrentPrice = maxCurrentPrice; 

        }

    }
}
