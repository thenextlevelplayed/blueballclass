namespace Logs;

public abstract class Exporter
{
    public abstract void Export(string format);
}

public class CompositeExporter : Exporter
{
    private List<Exporter> _exporters;

    public CompositeExporter(List<Exporter> exporters)
    {
        _exporters = exporters;
    }

    public override void Export(string format)
    {
        foreach (var exporter in _exporters)
        {
            exporter.Export(format);
        }
    }
}

public class FileExporter : Exporter
{
    private string _FileName;

    public FileExporter(string file)
    {
        _FileName = file;
    }

    public override void Export(string format)
    {
        File.AppendAllText(_FileName, format);
    }
}

public class ConsoleExporter : Exporter
{
    public override void Export(string format)
    {
        Console.WriteLine(format);
    }
}