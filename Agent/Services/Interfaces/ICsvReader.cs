using System;
using System.Threading.Tasks;
using Agent.Models;

namespace Agent.Services.Interfaces
{
    public interface ICsvReader : IDisposable
    {
        Task<AggregatedData?> Read();
    }
}