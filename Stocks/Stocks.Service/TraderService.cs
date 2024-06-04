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
    public class TraderService : IService<Trader>
    {
        private TraderRepository traderRepository;
        public TraderService(string connectionString)
        {
            traderRepository = new TraderRepository(connectionString);
        }
        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Trader> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Trader>> GetAll()
        {
            return await traderRepository.GetAll();
        }

        public Task<int> Post(Stock stock)
        {
            throw new NotImplementedException();
        }

        public Task<int> Put(Stock stock, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
