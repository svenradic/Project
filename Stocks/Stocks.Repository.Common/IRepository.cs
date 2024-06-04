using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocks.Model;

namespace Stocks.Repository.Common
{
    public interface IRepository<T> where T : class
    {
        Task<ICollection<T>> GetAll();
        Task<T> Get(Guid id);
        Task<int> Post(Stock stock);
        Task<int> Put(Stock stock, Guid id);
        Task<int> Delete(Guid id);
    }
}
