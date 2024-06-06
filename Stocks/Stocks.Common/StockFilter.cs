using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Common
{
    public class StockFilter: IFilter
    {
        public Guid? Id { get; set; }
        public string SymbolQuery { get; set; }
        public string CompanyQuery { get; set; }
        public long? MaxMarketCap { get; set; }
        public long? MinMarketCap { get; set; }
        public double? MinCurrentPrice { get; set; }
        public double? MaxCurrentPrice { get; set; }
        public string Name { get; set; }
        public DateTime? MinDateOfBirth { get; set; }
        public DateTime? MaxDateOfBirth { get; set; }

        public StockFilter(Guid? id, string symbolQuery, string companyQuery, long? minMarketCap, long? maxMarketCap, 
            double? minCurrentPrice, double? maxCurrentPrice)
        {
            this.Id = id;
            this.SymbolQuery = symbolQuery;
            this.CompanyQuery = companyQuery;
            this.MaxMarketCap = maxMarketCap;
            this.MinMarketCap = minMarketCap;
            this.MinCurrentPrice = minCurrentPrice;
            this.MaxCurrentPrice = maxCurrentPrice;
            Name = "";
            MinDateOfBirth = null;
            MaxDateOfBirth = null;
        }

    }
}
