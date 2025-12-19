namespace Service_Discovery___Load_Balancing;

public class HttpRequest
{
    public string Url { get; set; }
    private Random Random = new Random();

    public HttpRequest(string url)
    {
        this.Url = url;
    }

    public string Get()
    {
        var dic = Random.Next(0, 100);
        if (dic % 2 > 0)
        {
            return $"[SUCCESS]{Url}";
        }
        else
        {
            throw new ArgumentException("Ah on.");
        }
    }
}