using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Api.Contract.Interfaces.Services;

namespace StockManagement.Api.Controllers
{
    [Authorize]
    [Route("api/stocks")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStocksService _stocksService;

        public StocksController(IStocksService stocksService)
        {
            _stocksService = stocksService;
        }

        [HttpGet(Name = "GetStocks")]
        public async Task<IActionResult> Get()
        {
            var stocks = await _stocksService.GetStocks();

            if (stocks.Count == 0)
            {
                return NoContent();
            }

            return Ok(stocks);
        }
    }
}