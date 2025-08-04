using RPG.ActionOption;

namespace RPG;

public class Rpg
{
    private Troop _t1;
    private Troop _t2;

    public Rpg(Troop t1, Troop t2)
    {
        _t1 = t1;
        t1.Rpg = this;
        _t2 = t2;
        t2.Rpg = this;
    }

    public void Battle()
    {
    }

    public bool Annihilate(Troop troop)
    {
        return !troop.Roles.Any();
    }

    public IActionOption S1()
    {
        return null;
    }

    public List<Role> S2()
    {
        return null;
    }

    public void S3(IActionOption actionOption)
    {
    }

    public void UpdateTroop(Troop troop)
    {
        troop.Roles.RemoveAll(r => r.Hp <= 0);
    }
}