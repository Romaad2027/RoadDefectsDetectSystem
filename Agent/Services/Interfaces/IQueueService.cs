using System.Threading.Tasks;

namespace Agent.Services.Interfaces
{
    public interface IQueueService
    {
        Task Publish<T>(T message);
    }
}