using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocks.Common;
using Stocks.Model;
using Stocks.Repository;
using Stocks.Repository.Common;
using Stocks.Service.Common;

namespace Stocks.Service
{
    public class TraderService : IService<Trader>
    {
        private IRepository<Trader> _traderRepository;
        public TraderService(IRepository<Trader> treaderRepository)
        {
            this._traderRepository = treaderRepository;
        }

        public async Task<Trader?> GetAsync(Guid? id)
        {
            return await _traderRepository.GetAsync(id);
        }
        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<int> PostAsync(Stock stock)
        {
            throw new NotImplementedException();
        }

        public Task<int> PutAsync(Stock stock, Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Trader>> GetAsync(IFilter filter, SortingParameters order, PageFilter page)
        {
            return await _traderRepository.GetAsync(filter, order, page);
        }
    }
}
