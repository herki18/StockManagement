using System.Collections.Generic;
using System.Threading.Tasks;
using StockManagement.Api.Contract.Entities;

namespace StockManagement.Api.Contract.Interfaces.Repositories
{
    public interface IFruitsRepository
    {
        Task<IList<FruitEntity>> GetFruits();
    }
}