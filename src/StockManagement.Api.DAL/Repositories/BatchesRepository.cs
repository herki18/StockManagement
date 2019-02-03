using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockManagement.Api.Contract.Entities;
using StockManagement.Api.Contract.Interfaces.Repositories;

namespace StockManagement.Api.DAL.Repositories
{
    public class BatchesRepository : IBatchesRepository
    {
        private readonly ApiContext _context;
        private readonly ILogger<BatchesRepository> _logger;

        public BatchesRepository(ApiContext context, ILogger<BatchesRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IList<BatchEntity>> GetBatches()
        {
            return await _context.Batches
                .Include(x => x.Fruit)
                .ThenInclude(i => i.Variety)
                .ToListAsync();
        }

        public async Task<BatchEntity> Create(BatchEntity batch)
        {
            try
            {
                if (batch != null)
                {
                    if (batch.FruitId == 0)
                    {
                        throw new ArgumentException("Fruit is invalid.");
                    }

                    _context.Batches.Add(batch);

                    if (!(await _context.SaveChangesAsync() >= 0))
                    {
                        throw new Exception("Creating batch failed on save.");
                    }

                    return await GetBatchById(batch.Id);
                }

                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<BatchEntity> GetBatchById(int id)
        {
            return await _context.Batches
                .Include(x => x.Fruit)
                .ThenInclude(v => v.Variety)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<BatchEntity> Update(int id, BatchEntity batch)
        {
            try
            {
                if (batch != null)
                {
                    if (batch.FruitId == 0)
                    {
                        throw new ArgumentException("Fruit is invalid.");
                    }

                    _context.Batches.Update(batch);

                    if (!(await _context.SaveChangesAsync() >= 0))
                    {
                        throw new Exception("Creating batch failed on save.");
                    }

                    return await GetBatchById(batch.Id);
                }

                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var batch = await GetBatchById(id);

                if (batch == null)
                {
                    throw new Exception("Batch does not exist");
                }

                _context.Batches.Remove(batch);

                if (!(await _context.SaveChangesAsync() >= 0))
                {
                    throw new Exception("Deleting batch failed on save.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<IList<IGrouping<int, BatchEntity>>> GetStocks()
        {
            var groupedStocks = await _context.Batches
                .Include(x => x.Fruit)
                .ThenInclude(v => v.Variety)
                .GroupBy(x => x.FruitId).ToListAsync();
            return groupedStocks;
        }
    }
}