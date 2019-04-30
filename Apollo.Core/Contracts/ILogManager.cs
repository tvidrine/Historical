using System;

namespace Apollo.Core.Contracts
{
    public interface ILogManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(Exception ex, string message);
    }
}