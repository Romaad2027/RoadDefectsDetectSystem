using System.Threading.Tasks;
using Hub.Models;
using Hub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Controllers
{
    [ApiController]
    [Route("api/processed-agent-data")]
    public class HubController : ControllerBase
    {
        private readonly IAgentDataService _agentDataService;

        public HubController(IAgentDataService service)
        {
            _agentDataService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProcessedAgentData data)
        {
            await _agentDataService.Save(data);
            return Ok();
        }
    }
}