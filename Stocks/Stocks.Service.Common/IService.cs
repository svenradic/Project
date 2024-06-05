using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocks.Model;

namespace Stocks.Service.Common
{
    public interface IService<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<int> PostAsync(Stock stock);
        Task<int> PutAsync(Stock stock, Guid id);
        Task<int> DeleteAsync(Guid id);
    }
}
