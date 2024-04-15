using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Exceptions;
using Store.Models;
using Store.Services.Interfaces;

namespace Store.Controllers
{
    [ApiController]
    [Route("api/processed-agent-data")]
    public class AgentDataController : ControllerBase
    {
        private readonly IProcessedAgentDataService _processedAgentDataService;

        public AgentDataController(IProcessedAgentDataService processedAgentDataService)
        {
            _processedAgentDataService = processedAgentDataService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProcessedAgentData(int id)
        {
            try
            {
                var data = await _processedAgentDataService.GetProcessedAgentData(id);
                return Ok(data);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProcessedAgentData(int id)
        {
            try
            {
                await _processedAgentDataService.DeleteProcessedAgentData(id);
                return Ok();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProcessedAgentData(int id, [FromBody] ProcessedAgentDataRequestModel requestData)
        {
            try
            {
                await _processedAgentDataService.UpdateProcessedAgentData(id, requestData);
                return Ok(requestData);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProcessedAgentData([FromBody] ProcessedAgentDataRequestModel requestData)
        {
            var addedData = await _processedAgentDataService.AddProcessedAgentData(requestData);
            return Ok(addedData);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> BulkAddProcessedAgentData([FromBody] BulkAddAgentDatasRequestModel model)
        {
            await _processedAgentDataService.BulkAddProcessedAgentData(model.Models);
            return Ok();
        }
    }
}
