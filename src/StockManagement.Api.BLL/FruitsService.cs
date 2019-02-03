using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using StockManagement.Api.Contract.Interfaces.Repositories;
using StockManagement.Api.Contract.Interfaces.Services;
using StockManagement.Api.Contract.Models;

namespace StockManagement.Api.BLL
{
    public class FruitsService : IFruitsService
    {
        private readonly IFruitsRepository _fruitsRepository;
        private readonly IMapper _mapper;

        public FruitsService(IFruitsRepository fruitsRepository, IMapper mapper)
        {
            _fruitsRepository = fruitsRepository;
            _mapper = mapper;
        }

        public async Task<IList<FruitDTO>> GetFruits()
        {
            var entities = await _fruitsRepository.GetFruits();
            return _mapper.Map<List<FruitDTO>>(entities);
        }
    }
}