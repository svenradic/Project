using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Common
{
    public class OrderByFilter
    {
        public string orderBy;
        public string sortOrder;
        public OrderByFilter(string orderBy, string sortOrder)
        {
            this.orderBy = orderBy;
            this.sortOrder = sortOrder;
        }
    }
}
