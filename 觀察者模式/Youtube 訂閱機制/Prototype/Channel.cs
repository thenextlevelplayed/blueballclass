namespace Youtube_訂閱機制;

public class Channel
{
    public Channel(string name)
    {
        _name = name;
    }

    private List<Video> _videos = new List<Video>();

    private List<ChannelSubscriber> _subscribers = new List<ChannelSubscriber>();

    public string _name { get; set; }

    public void Upload(Video video)
    {
        this._videos.Add(video);
        video.Uploaded(this);
        Console.WriteLine($"頻道 {_name} 上架了一則新影片 \"{video._title}\"。");
        Notify(video);
    }

    public void Notify(Video video)
    {
        var subscribersCopy = new List<ChannelSubscriber>(_subscribers); // 创建副本
        foreach (var subscriber in subscribersCopy)
        {
            subscriber.GetNotification(video);
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
}