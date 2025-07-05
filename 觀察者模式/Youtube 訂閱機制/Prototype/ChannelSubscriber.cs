namespace Youtube_訂閱機制;

public class ChannelSubscriber
{
    public ChannelSubscriber(string name)
    {
        _name = name;
    }

    private string _name { get; set; }
    private List<Channel> _channels { get; set; } = new List<Channel>();

    public void Subscribe(Channel channel)
    {
        _channels.Add(channel);
        channel.Subscribed(this);
        Console.WriteLine($"{_name} 訂閱了 {channel._name}。");
    }

    public void Unsubscribe(Channel channel)
    {
        _channels.Remove(channel);
        channel.Unsubscribed(this);
        Console.WriteLine($"{_name} 解除訂閱了 {channel._name}");
    }

    public void GetNotification(Video video)
    {
        Action(video);
    }

    public void Action(Video video)
    {
        if (video._length >= 180)
        {
            GiveLike(video);
            Console.WriteLine($"{_name} 對影片 \"{video._title}\" 按讚。");
        }

        if (video._length <= 60)
        {
            Unsubscribe(video._channel);
        }
    }

    public void GiveLike(Video video)
    {
        video.GetLike();
    }
}