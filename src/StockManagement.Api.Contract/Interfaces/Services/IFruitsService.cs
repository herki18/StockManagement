using System.Collections.Generic;
using System.Threading.Tasks;
using StockManagement.Api.Contract.Models;

namespace StockManagement.Api.Contract.Interfaces.Services
{
    public interface IFruitsService
    {
        Task<IList<FruitDTO>> GetFruits();
    }
}