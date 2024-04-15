using System.Threading.Tasks;

namespace Store.DAL
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
        ProcessedAgentDataRepository ProcessedAgentDataRepository { get; }
    }
}