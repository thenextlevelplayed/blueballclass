namespace Youtube_訂閱機制.Observer;

public class FireBallSubscriber : ChannelSubscriber
{
    public FireBallSubscriber(string name) : base(name)
    {
    }

    public override void Action(Channel channel, Video video)
    {
        if (video.Length <= 60)
        {
            this.Unsubscribe(video.Channel);
        }
    }
}