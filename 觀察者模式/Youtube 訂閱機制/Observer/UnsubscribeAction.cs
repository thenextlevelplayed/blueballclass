namespace Youtube_訂閱機制.Observer;

public class UnsubscribeAction:IUploadObserver
{
    public void Action(Video video,ChannelSubscriber subscriber)
    {
        if (video.Length <= 60)
        {
            subscriber.Unsubscribe(video.Channel);
        }
    }
}