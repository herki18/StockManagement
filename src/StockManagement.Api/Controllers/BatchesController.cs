using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Api.Contract.Interfaces.Services;
using StockManagement.Api.Contract.Models;
using StockManagement.Api.Contract.Models.Batch;

namespace StockManagement.Api.Controllers
{
    [Authorize]
    [Route("api/batches")]
    [ApiController]
    public class BatchesController : ControllerBase
    {
        private readonly IBatchesService _batchesService;

        public BatchesController(IBatchesService batchesService)
        {
            _batchesService = batchesService;
        }

        [HttpGet(Name = "GetBatches")]
        public async Task<IActionResult> GetBatches()
        {
            var batches = await _batchesService.GetBatches();

            if (batches.Count == 0)
            {
                return NoContent();
            }

            return Ok(batches);
        }

        [HttpGet("{id}", Name = "GetBatch")]
        public async Task<IActionResult> GetBatch(int id)
        {
            var batch = await _batchesService.GetBatchById(id);
            return Ok(batch);
        }

        [HttpPost(Name = "PostBatch")]
        public async Task<IActionResult> Post([FromBody] BatchForCreate batch)
        {
            var created = await _batchesService.Create(batch);

            return CreatedAtAction("GetBatch", new {id = created.Id}, created);
        }

        [HttpPut("{id}", Name = "PutBatch")]
        public async Task<IActionResult> Put(int id, [FromBody] BatchForUpdate batch)
        {
            var updated = await _batchesService.Update(id, batch);

            return CreatedAtAction("GetBatch", new {id = updated.Id}, updated);
        }

        [HttpDelete("{id}", Name = "DeleteBatch")]
        public async Task<OkResult> Delete(int id)
        {
            await _batchesService.Delete(id);
            return Ok();
        }
    }
}