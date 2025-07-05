namespace Youtube_訂閱機制.Observer;

public class WaterBallSubscriber : ChannelSubscriber
{
    public WaterBallSubscriber(string name) : base(name)
    {
    }

    public override void Action(Channel channel, Video video)
    {
        if (video.Length >= 180)
        {
            this.GiveLike(video);
            Console.WriteLine($"{this.Name} 對影片 \"{video.Title}\" 按讚。");
        }
    }
}