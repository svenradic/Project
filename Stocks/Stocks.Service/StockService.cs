using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocks.Common;
using Stocks.Model;
using Stocks.Repository.Common;
using Stocks.Service.Common;

namespace Stocks.Service
{
    public class StockService: IService<Stock>
    {
        private IRepository<Stock> stockRepository;
        public StockService(IRepository<Stock> stockRepository)
        {
            this.stockRepository = stockRepository;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            return await stockRepository.DeleteAsync(id);
        }
        public async Task<ICollection<Stock>> GetAsync(IFilter filter, OrderByFilter order, PageFilter page)
        {
            return await stockRepository.GetAsync(filter, order, page);
        }

        public async Task<int> PostAsync(Stock stock)
        {
            return await stockRepository.PostAsync(stock);
        }

        public async Task<int> PutAsync(Stock stock, Guid id)
        {
            return await stockRepository.PutAsync(stock, id);
        }

    }
}
