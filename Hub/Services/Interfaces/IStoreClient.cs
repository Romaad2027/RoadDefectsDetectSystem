using System.Collections.Generic;
using System.Threading.Tasks;
using Hub.Models;

namespace Hub.Services.Interfaces
{
    public interface IStoreClient
    {
        Task BulkAdd(IEnumerable<ProcessedAgentData> datas);
    }
}