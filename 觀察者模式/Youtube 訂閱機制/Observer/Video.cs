namespace Youtube_訂閱機制.Observer;

public class Video
{
    public Video(string title, string description, int length, Channel channel, int like = 0)
    {
        Title = title;
        Description = description;
        Length = length;
        Channel = channel;
        Like = like;
    }

    public string Title { get; set; }
    private string Description { get; set; }
    public int Length { get; set; }
    public int Like { get; set; }
    public Channel Channel { get; set; }

    public void GetLike()
    {
        Like++;
    }
}