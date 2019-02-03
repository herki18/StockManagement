using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using StockManagement.Api.Contract.Interfaces.Repositories;
using StockManagement.Api.Contract.Interfaces.Services;
using StockManagement.Api.Contract.Models;

namespace StockManagement.Api.BLL
{
    public class StocksService : IStocksService
    {
        private readonly IMapper _mapper;
        private readonly IBatchesRepository _repository;

        public StocksService(IBatchesRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IList<StockDTO>> GetStocks()
        {
            var entity = await _repository.GetStocks();


            return _mapper.Map<List<StockDTO>>(entity);
        }
    }
}