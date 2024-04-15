namespace Agent.Services.Interfaces
{
    public interface ICommonLogger
    {
        void Information(string message);
        void Error(string message);
        void Warning(string message);
    }
}