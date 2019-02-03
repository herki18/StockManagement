using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using StockManagement.Api.Contract.Entities;
using StockManagement.Api.Contract.Interfaces.Repositories;
using StockManagement.Api.Contract.Interfaces.Services;
using StockManagement.Api.Contract.Models;
using StockManagement.Api.Contract.Models.Batch;

namespace StockManagement.Api.BLL
{
    public class BatchesService : IBatchesService
    {
        private readonly IBatchesRepository _batchesRepository;
        private readonly IMapper _mapper;

        public BatchesService(IBatchesRepository batchesRepository, IMapper mapper)
        {
            _batchesRepository = batchesRepository;
            _mapper = mapper;
        }

        public async Task<IList<BatchDTO>> GetBatches()
        {
            var entities = await _batchesRepository.GetBatches();
            return _mapper.Map<List<BatchDTO>>(entities);
        }

        public async Task<BatchDTO> Create(BatchForCreate batch)
        {
            var entity = _mapper.Map<BatchEntity>(batch);
            var createdEntity = await _batchesRepository.Create(entity);

            return _mapper.Map<BatchDTO>(createdEntity);
        }

        public async Task<BatchDTO> GetBatchById(int id)
        {
            var entity = await _batchesRepository.GetBatchById(id);
            return _mapper.Map<BatchDTO>(entity);
        }

        public async Task<BatchDTO> Update(int id, BatchForUpdate batch)
        {
            var entity = _mapper.Map<BatchEntity>(batch);
            entity.Id = id;

            var updatedEntity = await _batchesRepository.Update(id, entity);

            return _mapper.Map<BatchDTO>(updatedEntity);
        }

        public async Task Delete(int id)
        {
            await _batchesRepository.Delete(id);
        }
    }
}