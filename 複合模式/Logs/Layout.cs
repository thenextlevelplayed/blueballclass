namespace Logs;

public interface Layout
{
    string Format(Logger logger, string threshold, string message);
}

public class Standard : Layout
{
    public string Format(Logger logger, string threshold, string message)
    {
        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        return $"{time} |-{threshold} {logger.Name} - {message}";
    }
}