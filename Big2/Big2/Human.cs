namespace Big2;

public class Human(int index) : Player(index)
{
    protected override string GetName()
    {
        string nameInput = Console.ReadLine();
        return !string.IsNullOrEmpty(nameInput) ? nameInput : GetName();
    }
}