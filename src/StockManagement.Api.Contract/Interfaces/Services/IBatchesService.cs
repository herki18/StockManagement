using System.Collections.Generic;
using System.Threading.Tasks;
using StockManagement.Api.Contract.Models;
using StockManagement.Api.Contract.Models.Batch;

namespace StockManagement.Api.Contract.Interfaces.Services
{
    public interface IBatchesService
    {
        Task<IList<BatchDTO>> GetBatches();
        Task<BatchDTO> Create(BatchForCreate batch);
        Task<BatchDTO> GetBatchById(int id);
        Task<BatchDTO> Update(int id, BatchForUpdate batch);
        Task Delete(int id);
    }
}