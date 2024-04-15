using System.Threading.Tasks;
using Hub.Models;

namespace Hub.Services.Interfaces
{
    public interface IAgentDataService
    {
        Task Save(ProcessedAgentData data);
    }
}