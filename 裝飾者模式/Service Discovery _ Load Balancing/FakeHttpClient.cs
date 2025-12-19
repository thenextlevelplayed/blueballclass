namespace Service_Discovery___Load_Balancing;

public class FakeHttpClient : HttpService
{
    public string Process(HttpRequest request)
    {
        return request.Get();
    }
}