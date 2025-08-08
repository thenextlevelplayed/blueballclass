using RPG.Command;

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
        while (!Annihilate(_t1) && !Annihilate(_t2))
        {
            foreach (var role in  _t1.Roles.ToList())
            {
                if (role.CanAction())
                {
                    role.HandleStartOfTurn();
                    var s1 = S1(role);
                    var s2 = S2(role, s1);
                    S3(role, s1, s2);
                    role.HandleEndOfTurn();
                    role.NotifyObservers();
                }
                UpdateTroop(_t1);
                UpdateTroop(_t2);
            }
            
            foreach (var role in _t2.Roles.ToList())
            {
                if (role.CanAction())
                {
                    role.HandleStartOfTurn();
                    var s1 = S1(role);
                    var s2 = S2(role, s1);
                    S3(role, s1, s2);
                    role.HandleEndOfTurn();
                    role.NotifyObservers();
                }
                UpdateTroop(_t1);
                UpdateTroop(_t2);
            }
        }
        var log = Annihilate(_t1) ? "你失敗了！" : "你獲勝了！";
        Console.WriteLine(log);
    }


    private bool Annihilate(Troop troop)
    {
        return !troop.Roles.Any();
    }

    private ICommand S1(Role role)
    {
        return role.S1();
    }

    private List<Role> S2(Role role, ICommand s1)
    {
        return role.S2(s1);
    }

    private void S3(Role role, ICommand command, List<Role> roles)
    {
        role.S3(command, roles);
    }

    public void UpdateTroop(Troop troop)
    {
        troop.Roles.RemoveAll(r => r.Hp <= 0);
    }
}