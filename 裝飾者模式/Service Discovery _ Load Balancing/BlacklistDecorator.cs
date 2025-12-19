namespace Service_Discovery___Load_Balancing;

public class BlacklistDecorator : HttpClientDecorator
{
    private List<string> BlockListIps;

    public BlacklistDecorator(string config, HttpService? nextService) : base(config, nextService)
    {
        GetBlockList();
    }

    public override string Process(HttpRequest request)
    {
        var domainBlockList =
            BlockListIps.Select(m => m == new Uri(request.Url).Host).Any(); // when host is not ip but domain name 
        var ipInfo = IpInfos
            .FirstOrDefault(m => m.IpAddresses.Contains(new Uri(request.Url).Host)); // when host is ip address
        if (domainBlockList) throw new ArgumentException("Domain is blocked.");

        if (ipInfo != null && BlockListIps.Contains(ipInfo.DomainName))
        {
            throw new ArgumentException("The IP's domain is blocked.");
        }

        return NextService!.Process(request);
    }

    private void GetBlockList()
    {
        var text = File.ReadAllText(Config);
        BlockListIps = text.Split(',').Select(m => m.Trim()).ToList();
    }
}