namespace Big2;

public class AI(int index) : Player(index)
{
    protected override string GetName()
    {
       return $"Ai{index}";
    }
}