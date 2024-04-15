using System.Threading.Tasks;

namespace Store.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            dataContext.Database.EnsureCreated();

            _dataContext = dataContext;
            ProcessedAgentDataRepository = new ProcessedAgentDataRepository(dataContext.ProcessedAgentDatas);
        }

        public async Task SaveChanges()
        {
            await _dataContext.SaveChangesAsync();
        }

        public ProcessedAgentDataRepository ProcessedAgentDataRepository { get; }
    }
}