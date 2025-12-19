namespace Service_Discovery___Load_Balancing;

public class IpInfo
{
    public string DomainName { get; set; }
    public List<string> IpAddresses { get; set; }
    public Dictionary<string, DateTime> UselessIpAddresses { get; set; } = new Dictionary<string, DateTime>();
    public int CurrentIndex { get; set; }

    public IpInfo(string domainName, List<string> ipAddresses)
    {
        this.DomainName = domainName;
        this.IpAddresses = ipAddresses;
        this.CurrentIndex = 0;
    }

    public List<string> GetValidIps()
    {
        var validIps = new List<string>();
        UselessIpAddresses.Where(m => DateTime.Now > m.Value.AddMinutes(10)).ToList().ForEach(m => UselessIpAddresses.Remove(m.Key));
        return IpAddresses.Where(m => !UselessIpAddresses.ContainsKey(m)).ToList();
    }
}