using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockManagement.Api.Contract.Entities;
using StockManagement.Api.Contract.Interfaces.Repositories;

namespace StockManagement.Api.DAL.Repositories
{
    public class FruitsRepository : IFruitsRepository
    {
        private readonly ApiContext _context;

        public FruitsRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IList<FruitEntity>> GetFruits()
        {
            return await _context.Fruits.Include(x => x.Variety).ToListAsync();
        }
    }
}