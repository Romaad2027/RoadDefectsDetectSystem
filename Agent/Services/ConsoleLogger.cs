using System;
using Agent.Services.Interfaces;

namespace Agent.Services
{
    public class ConsoleLogger : ICommonLogger
    {
        public void Information(string message)
        {
            Console.WriteLine($"Information: {message}");
        }

        public void Error(string message)
        {
            Console.WriteLine($"Error: {message}");
        }

        public void Warning(string message)
        {
            Console.WriteLine($"Warning: {message}");
        }
    }
}