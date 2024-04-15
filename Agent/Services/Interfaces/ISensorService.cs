using System.Threading.Tasks;

namespace Agent.Services.Interfaces
{
    public interface ISensorService
    {
        Task PublishSensorsData();
    }
}