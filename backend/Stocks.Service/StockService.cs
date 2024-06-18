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
        private IRepository<Stock> _stockRepository;
        public StockService(IRepository<Stock> stockRepository)
        {
            this._stockRepository = stockRepository;
        }
        public async Task<Stock> GetAsync(Guid? id)
        {
            return await _stockRepository.GetAsync(id);
        }
        public async Task<int> DeleteAsync(Guid id)
        {
            return await _stockRepository.DeleteAsync(id);
        }
        public async Task<ICollection<Stock>> GetAsync(IFilter filter, SortingParameters order, PageFilter page)
        {
            return await _stockRepository.GetAsync(filter, order, page);
        }

        public async Task<int> PostAsync(Stock stock)
        {
            return await _stockRepository.PostAsync(stock);
        }

        public async Task<int> PutAsync(Stock stock, Guid id)
        {
            stock.UpdatedAt = DateTime.Now;
            return await _stockRepository.PutAsync(stock, id);
        }

    }
}
