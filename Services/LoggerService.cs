using Microsoft.Extensions.Logging;

namespace EmployeeTemperatures.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> logger;
        public LoggerService(ILogger<LoggerService> _logger)
        {
            logger = _logger;
        }
        public void LogExecute(string methodName, string message)
        {
            logger.LogInformation(methodName + " " + message + " - executing");
        }

        public void LogExecutionResult(bool condition, string methodName, string successMessage, string failedMessage)
        {
            if(condition)
            {
                LogSuccess(methodName, successMessage);
            } else
            {
                LogFailed(methodName, failedMessage);
            }
        }

        private void LogFailed(string methodName, string message)
        {
            logger.LogWarning(methodName + " " + message + " - failed");
        }

        private void LogSuccess(string methodName, string message)
        {
            logger.LogInformation(methodName + " " + message + " - success");
        }
    }
}
