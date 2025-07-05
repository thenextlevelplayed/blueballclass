namespace Youtube_訂閱機制.Observer;

public class Channel
{
    public Channel(string name)
    {
        Name = name;
    }

    private List<Video> _videos = new List<Video>();

    private List<ChannelSubscriber> _subscribers = new List<ChannelSubscriber>();


    public string Name { get; set; }

    public void Upload(Video video)
    {
        this._videos.Add(video);
        Console.WriteLine($"頻道 {Name} 上架了一則新影片 \"{video.Title}\"。");
        Notify(video);
    }

    private void Notify(Video video)
    {
        var subscribers = new List<ChannelSubscriber>(_subscribers);
        foreach (var subscriber in subscribers)
        {
            subscriber.Action(this, video);
        }
    }

    public void Subscribed(ChannelSubscriber subscriber)
    {
        _subscribers.Add(subscriber);
    }

    public void Unsubscribed(ChannelSubscriber subscriber)
    {
        _subscribers.Remove(subscriber);
    }

    public void Register(ChannelSubscriber subscriber)
    {
        if (!_subscribers.Contains(subscriber)) // 最好加上重複檢查
        {
            _subscribers.Add(subscriber);
        }
    }

    public void Unregister(ChannelSubscriber subscriber)
    {
        _subscribers.Remove(subscriber);
    }
}