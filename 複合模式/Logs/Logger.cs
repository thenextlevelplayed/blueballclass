namespace Logs;

public class Logger
{
    public LevelThreshold? Level { get; set; }
    private Logger? Parent { get; set; } = null;
    public string? Name { get; set; } = null;
    public Exporter? Exporter { get; set; } = null;
    public Layout? Layout { get; set; } = null;

    public Logger(LevelThreshold? level, Logger? parent = null, string? name = null, Exporter? exporter = null,
        Layout? layout = null)
    {
        Level = level;
        Parent = parent;
        Name = name ?? "Root";
        Exporter = parent != null && exporter == null ? parent.Exporter : exporter;
        Layout = parent != null && Layout == null ? parent.Layout : layout;
    }

    public void Trace(string message)
    {
        string logInfo = Layout.Format(this, "TRACE", message);
        Exporter.Export(logInfo);
    }

    public void Error(string message)
    {
        if (LevelThreshold.Error >= Level)
        {
            string logInfo = Layout.Format(this, "ERROR", message);
            Exporter.Export(logInfo);
        }
    }

    public void Warn(string message)
    {
        if (LevelThreshold.Warn >= Level)
        {
            string logInfo = Layout.Format(this, "WARN", message);
            Exporter.Export(logInfo);
        }
    }

    public void Debug(string message)
    {
        if (LevelThreshold.Debug >= Level)
        {
            string logInfo = Layout.Format(this, "DEBUG", message);
            Exporter.Export(logInfo);
        }
    }

    public void Info(string message)
    {
        if (LevelThreshold.Info >= Level)
        {
            string logInfo = Layout.Format(this, "INFO", message);
            Exporter.Export(logInfo);
        }
    }
}