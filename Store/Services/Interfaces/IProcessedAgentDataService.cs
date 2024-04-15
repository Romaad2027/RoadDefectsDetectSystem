using System.Collections.Generic;
using System.Threading.Tasks;
using Store.DAL.Entities;
using Store.Models;

namespace Store.Services.Interfaces
{
    public interface IProcessedAgentDataService
    {
        Task<ProcessedAgentData> GetProcessedAgentData(int id);
        Task DeleteProcessedAgentData(int id);
        Task<ProcessedAgentData> AddProcessedAgentData(ProcessedAgentDataRequestModel data);
        Task BulkAddProcessedAgentData(IEnumerable<ProcessedAgentDataRequestModel> data);
        Task UpdateProcessedAgentData(int id, ProcessedAgentDataRequestModel data);
    }
}