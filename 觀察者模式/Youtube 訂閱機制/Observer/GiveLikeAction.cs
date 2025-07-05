namespace Youtube_訂閱機制.Observer;

public class GiveLikeAction:IUploadObserver
{
    public void Action(Video video,ChannelSubscriber subscriber)
    {
        if (video.Length >= 180)
        {
            subscriber.GiveLike(video);
            Console.WriteLine($"{subscriber.Name} 對影片 \"{video.Title}\" 按讚。");
        }
    }
}