using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocks.Model;
using Stocks.Repository;
using Stocks.Service.Common;

namespace Stocks.Service
{
    public class StockService: IService<Stock>
    {
        private StockRepository stockRepository;
        public StockService(string connectionString)
        {
            stockRepository = new StockRepository(connectionString);
        }
        public StockService(StockRepository stockRepository)
        {
            this.stockRepository = stockRepository;
        }

        public async Task<int> Delete(Guid id)
        {
            return await stockRepository.Delete(id);
        }

        public async Task<Stock> Get(Guid id)
        {
            return await stockRepository.Get(id);
        }

        public async Task<ICollection<Stock>> GetAll()
        {
            return await stockRepository.GetAll();
        }

        public async Task<int> Post(Stock stock)
        {
            return await stockRepository.Post(stock);
        }

        public async Task<int> Put(Stock stock, Guid id)
        {
            return await stockRepository.Put(stock, id);
        }

    }
}
