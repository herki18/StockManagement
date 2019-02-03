using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockManagement.Api.Contract.Entities;

namespace StockManagement.Api.Contract.Interfaces.Repositories
{
    public interface IBatchesRepository
    {
        Task<IList<BatchEntity>> GetBatches();
        Task<BatchEntity> Create(BatchEntity batch);
        Task<BatchEntity> GetBatchById(int id);
        Task<BatchEntity> Update(int id, BatchEntity entity);
        Task Delete(int id);
        Task<IList<IGrouping<int, BatchEntity>>> GetStocks();
    }
}