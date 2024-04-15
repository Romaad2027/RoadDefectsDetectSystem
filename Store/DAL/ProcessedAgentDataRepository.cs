using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.DAL.Entities;

namespace Store.DAL
{
    public class ProcessedAgentDataRepository
    {
        private readonly DbSet<ProcessedAgentData> _entries;

        public ProcessedAgentDataRepository(DbSet<ProcessedAgentData> entries)
        {
            _entries = entries;
        }

        public async Task<ProcessedAgentData?> GetById(int id)
        {
            return await _entries.FindAsync(id);
        }

        public async Task DeleteById(int id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                _entries.Remove(entity);
            }
        }

        public async Task<ProcessedAgentData> Add(ProcessedAgentData entry)
        {
            var addedEntry = await _entries.AddAsync(entry);
            return addedEntry.Entity;
        }

        public async Task Add(IEnumerable<ProcessedAgentData> entries)
        {
            await _entries.AddRangeAsync(entries);
        }

        public void Update(ProcessedAgentData entry)
        {
            _entries.Update(entry);
        }
    }
}
