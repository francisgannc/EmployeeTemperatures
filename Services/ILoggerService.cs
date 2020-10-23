namespace EmployeeTemperatures.Services
{
    public interface ILoggerService
    {
        void LogExecute(string methodName, string message);
        void LogExecutionResult(bool condition, string methodName, string successMessage, string failedMessage);
    }
}
