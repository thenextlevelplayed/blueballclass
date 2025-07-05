namespace Youtube_訂閱機制;

public class Video
{
    public Video(string title, string description, int length, int like = 0)
    {
        _title = title;
        _description = description;
        _length = length;
        _like = like;
    }

    public string _title { get; set; }
    private string _description { get; set; }
    public int _length { get; set; }
    public int _like { get; set; }
    public Channel _channel { get; set; }

    public void Uploaded(Channel channel)
    {
        this._channel = channel;
    }

    public void GetLike()
    {
        _like++;
    }
}