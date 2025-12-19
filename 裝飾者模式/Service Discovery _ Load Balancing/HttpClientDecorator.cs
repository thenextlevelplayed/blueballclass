namespace Service_Discovery___Load_Balancing;

public abstract class HttpClientDecorator : HttpService
{
    protected string Config;
    public abstract string Process(HttpRequest request);
    protected List<IpInfo> IpInfos;
    protected HttpService? NextService;
    public HttpClientDecorator(string config, HttpService? nextService)
    {
        Config = config;
        NextService = nextService;
        GetConfig();
    }

    protected virtual void GetConfig()
    {
        var text = File.ReadAllText(Config);
        var hostName = text.Split(":")[0].Trim();
        var ips = text.Split(":")[1].Trim().Split(',').ToList();
        IpInfo ipInfo = new IpInfo(hostName, ips);
        IpInfos = new List<IpInfo>() { ipInfo };
    }
}