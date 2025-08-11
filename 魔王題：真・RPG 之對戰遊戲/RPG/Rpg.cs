using RPG.Command;

namespace RPG;

public class Rpg
{
    private Troop _t1;
    private Troop _t2;
    private Queue<string> _inputQueue = new(); //測茲用

    public Rpg(Troop t1, Troop t2, IEnumerable<string> inputs)
    {
        _t1 = t1;
        t1.Rpg = this;
        _t2 = t2;
        t2.Rpg = this;
        _inputQueue = new Queue<string>(inputs);
    }

    public void Battle()
    {
        while (!Annihilate(_t1) && !Annihilate(_t2))
        {
            for (int i = 0; i < _t1.Roles.Count; i++)
            {
                var role = _t1.Roles[i];
                Console.WriteLine(
                    $"輪到 {role.Troop}{role.Name} (HP: {role.Hp}, MP: {role.Mp}, STR: {role.Str}, State: {role.State})。");
                role.HandleStartOfTurn();
                if (role.CanAction())
                {
                    var s1 = S1(role);
                    var s2 = S2(role, s1);
                    S3(role, s1, s2);
                }

                role.HandleEndOfTurn();
                UpdateTroop(_t1);
                UpdateTroop(_t2);
                if (Annihilate(_t1) || Annihilate(_t2))
                {
                    break;
                }
            }

            for (int i = 0; i < _t2.Roles.Count; i++)
            {
                var role = _t2.Roles[i];
                Console.WriteLine(
                    $"輪到 {role.Troop}{role.Name} (HP: {role.Hp}, MP: {role.Mp}, STR: {role.Str}, State: {role.State})。");
                role.HandleStartOfTurn();
                if (role.CanAction())
                {
                    var s1 = S1(role);
                    var s2 = S2(role, s1);
                    S3(role, s1, s2);
                }

                role.HandleEndOfTurn();
                UpdateTroop(_t1);
                UpdateTroop(_t2);
                if (Annihilate(_t1) || Annihilate(_t2))
                {
                    break;
                }
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
        for (int i = troop.Roles.Count - 1; i >= 0; i--)
        {
            Role role = troop.Roles[i];
            if (role.Hp <= 0)
            {
                role.NotifyObservers();
                Console.WriteLine($"{role.Troop}{role.Name} 死亡。");
                troop.Roles.RemoveAt(i);
            }
        }
    }

    public string ReadInput()
    {
        if (_inputQueue.Count > 0)
            return _inputQueue.Dequeue();
        return "0";
    }
}