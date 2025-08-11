namespace RPG.Observer;

public interface IObserver
{
    Role Role { get; set; }
    void Action();
}