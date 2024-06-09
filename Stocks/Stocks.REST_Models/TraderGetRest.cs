using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocks.Model;
using Stocks.WebAPI.RESTModels;

namespace Stocks.REST_Models
{
    public class TraderGetRest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public ICollection<StockGetRest> Stocks { get; set; }
    }
}
