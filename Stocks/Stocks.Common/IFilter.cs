using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Common
{
    public interface IFilter
    {
        Guid? Id { get; set; }
        string SymbolQuery { get; set; }
        string CompanyQuery { get; set; }
        long? MaxMarketCap { get; set; }
        long? MinMarketCap { get; set; }
        double? MinCurrentPrice { get; set; }
        double? MaxCurrentPrice {  get; set; }
        string Name { get; set; }
        DateTime? MinDateOfBirth { get; set; }
        DateTime? MaxDateOfBirth { get; set; }
    }
}
