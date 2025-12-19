namespace Service_Discovery___Load_Balancing;

public class LoadBalancingDecorator : HttpClientDecorator
{
    public LoadBalancingDecorator(string config, HttpService? nextService) : base(config, nextService)
    {
    }

    public override string Process(HttpRequest request)
    {
        var ip = IpInfos.FirstOrDefault(m => m.DomainName.Equals(new Uri(request.Url).Host));
        if (ip != null)
        {
            for (int i = 0; i < ip.IpAddresses.Count; i++)
            {
                ip.CurrentIndex = (ip.CurrentIndex + 1) % ip.IpAddresses.Count; // 更新索引
                if (ip.UselessIpAddresses.ContainsKey(ip.IpAddresses[ip.CurrentIndex]))
                {
                    var uselessTime = ip.UselessIpAddresses[ip.IpAddresses[ip.CurrentIndex]];
                    if (DateTime.Now > uselessTime.AddMinutes(10))
                    {
                        ip.UselessIpAddresses.Remove(ip.IpAddresses[ip.CurrentIndex]);
                    }
                    else
                    {
                        continue;
                    }
                }

                var uri = new Uri(request.Url);
                request.Url = $"{uri.Scheme}://{ip.IpAddresses[ip.CurrentIndex]}{uri.PathAndQuery}";
                try
                {
                    return NextService!.Process(request);
                }
                catch (Exception e)
                {
                    ip.UselessIpAddresses[ip.IpAddresses[ip.CurrentIndex]] = DateTime.Now;
                }
            }

            throw new ArgumentException("All IPs are unavailable.");
        }

        return NextService.Process(request);
    }
}