namespace DEMO.Prototype;

public class Hero:Sprite
{
    public Hero(World world) : base(world) { }
    private int hp { get; set; } = 30;

    protected override void IncreaseHp(int number)
    {
        hp+=number;
    }
    
    protected override void DecreaseHp(int number)
    {
        hp-=number;
    }
    
    public override string ToString()
    {
        return $"Hero Sprite";
    }

    protected override int GetHp() => hp;
}