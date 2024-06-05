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
        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Trader> GetAsync(Guid id)
        {
            return await traderRepository.GetAsync(id);
        }

        public async Task<ICollection<Trader>> GetAllAsync()
        {
            return await traderRepository.GetAllAsync();
        }

        public Task<int> PostAsync(Stock stock)
        {
            throw new NotImplementedException();
        }

        public Task<int> PutAsync(Stock stock, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
