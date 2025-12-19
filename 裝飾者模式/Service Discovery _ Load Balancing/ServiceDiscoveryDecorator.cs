namespace Service_Discovery___Load_Balancing;

public class ServiceDiscoveryDecorator : HttpClientDecorator
{
    public ServiceDiscoveryDecorator(string config, HttpService? nextService) : base(config, nextService)
    {
    }

    public override string Process(HttpRequest request)
    {
        var ipInfo = IpInfos.FirstOrDefault(m => m.DomainName.Equals(new Uri(request.Url).Host));
        if(ipInfo == null) return NextService.Process(request);
        var ips = ipInfo.GetValidIps();
        var ip = ips.FirstOrDefault();
        var uri = new Uri(request.Url);
        request.Url = $"{uri.Scheme}://{ip}{uri.PathAndQuery}";
        try
        {
            return NextService.Process(request);
        }
        catch (Exception e)
        {
            ipInfo.UselessIpAddresses[ip] = DateTime.Now;
            throw;
        }
    }
}