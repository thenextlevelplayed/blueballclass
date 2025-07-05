namespace Youtube_訂閱機制.Observer;

public abstract class ChannelSubscriber
{
    public ChannelSubscriber(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    protected List<Channel> Channels { get; set; } = new List<Channel>();

    // private readonly List<IUploadObserver> _uploadObservers = new List<IUploadObserver>();

    public void Subscribe(Channel channel)
    {
        Channels.Add(channel);
        channel.Subscribed(this);
        Console.WriteLine($"{Name} 訂閱了 {channel.Name}。");
    }
    
    public void GetNotification(Video video)
    {
        // foreach (var observer in _uploadObservers)
        // {
        //     observer.Action(video,this);
        // }
    }
  
    // public void Register(IUploadObserver observer)
    // {
    //     this._uploadObservers.Add(observer);
    // }
    //
    // public void Unregister(IUploadObserver observer)
    // {
    //     this._uploadObservers.Remove(observer);
    //
    // }
    
    public abstract void Action(Channel channel,Video video);
    public void GiveLike(Video video)
    {
        video.GetLike();
    }
    
    public void Unsubscribe(Channel channel)
    {
        Channels.Remove(channel);
        channel.Unsubscribed(this);
        Console.WriteLine($"{Name} 解除訂閱了 {channel.Name}");
    }
}