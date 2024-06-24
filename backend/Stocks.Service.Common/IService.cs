using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocks.Model;
using Stocks.Common;


namespace Stocks.Service.Common
{
    public interface IService<T> where T : class
    {
        Task<ICollection<T>> GetAsync(IFilter filter, SortingParameters order, PageFilter page);
        Task<T> GetAsync(Guid? id); 
        Task<int> PostAsync(T stock);
        Task<int> PutAsync(T stock, Guid id);
        Task<int> DeleteAsync(Guid id);
    }
}
