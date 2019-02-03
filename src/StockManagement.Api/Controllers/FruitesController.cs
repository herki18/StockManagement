using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Api.Contract.Interfaces.Services;

namespace StockManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsController : ControllerBase
    {
        private readonly IFruitsService _fruitsService;

        public FruitsController(IFruitsService fruitsService)
        {
            _fruitsService = fruitsService;
        }

        [HttpGet(Name = "GetFruits")]
        public async Task<IActionResult> Get()
        {
            var fruits = await _fruitsService.GetFruits();

            if (fruits.Count == 0)
            {
                return NoContent();
            }

            return Ok(fruits);
        }
    }
}