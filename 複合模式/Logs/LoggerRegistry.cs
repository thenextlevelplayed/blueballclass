using System.Collections.Concurrent;

namespace Logs;

public static class LoggerRegistry
{
    private static readonly ConcurrentDictionary<string, Logger> _ByName = new();

    public static void DeclareLoggers(Logger[] loggers)
    {
        foreach (var logger in loggers)
        {
            if (logger is null) return;
            if (!_ByName.TryAdd(logger.Name, logger))
            {
                throw new InvalidOperationException($"Logger with name '{logger.Name}' is already registered.");
            }
        }
    }

    public static Logger GetLogger(string logger)
    {
        _ByName.TryGetValue(logger, out var foundLogger);
        if (foundLogger is null)
        {
            throw new KeyNotFoundException($"Logger with name '{logger}' is not found.");
        }

        return foundLogger;
    }
}