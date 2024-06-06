using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Common
{
    public class TraderFilter : IFilter
    {
        public Guid? Id { get; set; }
        public string SymbolQuery { get; set; }
        public string CompanyQuery { get; set; }
        public long? MaxMarketCap { get; set; }
        public long? MinMarketCap { get; set ; }
        public double? MinCurrentPrice { get; set; }
        public double? MaxCurrentPrice { get; set; }
        public string Name { get; set; }
        public DateTime? MinDateOfBirth { get; set;}
        public DateTime? MaxDateOfBirth { get; set ;}

        public TraderFilter(Guid? id, string name, DateTime? minDateOfBirth, DateTime? maxDateOfBirth)
        {
            Id = id;
            Name = name;
            MinDateOfBirth = minDateOfBirth;
            MaxDateOfBirth = maxDateOfBirth;
            SymbolQuery = "";
            CompanyQuery = "";
            MaxMarketCap = null;
            MinMarketCap = null;
            MinCurrentPrice = null;
            MaxCurrentPrice = null;
        }
    }
}
