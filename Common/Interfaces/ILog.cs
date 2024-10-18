using Application.Enums;

namespace Application.Interfaces
{
    public interface ILog
    {
        public Task Log(Exception ex, LogLevel logLevel);   
    }
}
