using Application.Enums;
using Application.Interfaces;
using Infrastructure;
using Infrastructure.Models;
namespace Application.Auxiliary
{
    public class DatabaseLogger : ILog
    {

        private readonly Order2ServeDbContext _dbContext;

        public DatabaseLogger(Order2ServeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Log(Exception ex, LogLevel logLevel)
        {
            var log = new Log
            {
                OuterError = ex.Message,        
                InnerError = ex.InnerException?.Message,
                Level = logLevel.ToString(),
                Timestamp = DateTime.UtcNow
            };

            _dbContext.Logs.Add(log);
            await _dbContext.SaveChangesAsync();
        }
    }
}
