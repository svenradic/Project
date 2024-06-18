using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Common
{
    public class SortingParameters
    {
        public string orderBy;
        public string sortOrder;
        public SortingParameters(string orderBy, string sortOrder)
        {
            this.orderBy = orderBy;
            this.sortOrder = sortOrder;
        }
        public SortingParameters()
        {
            orderBy = "CreatedAt";
            sortOrder = "ASC";
        }
    }
}
