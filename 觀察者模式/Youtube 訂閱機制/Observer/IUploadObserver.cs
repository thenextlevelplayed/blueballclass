namespace Youtube_訂閱機制.Observer;

public interface IUploadObserver
{
    void Action(Video video, ChannelSubscriber subscriber);
}