using RPG.Command;

namespace RPG;

public class Rpg
{
    private readonly Troop _t1;
    private readonly Troop _t2;
    private readonly Queue<string> _inputQueue = new(); //測資用

    public Rpg(Troop t1, Troop t2, IEnumerable<string> inputs)
    {
        _t1 = t1;
        t1.Rpg = this;
        _t2 = t2;
        t2.Rpg = this;
        _inputQueue = new Queue<string>(inputs);
    }

    private bool ProcessTurn(Troop actingTroop, Troop opposingTroop)
    {
        for (int i = 0; i < actingTroop.Roles.Count; i++)
        {
            var role = actingTroop.Roles[i];
            if (role.Hp <= 0) continue;
            Console.WriteLine(
                $"輪到 {role.Troop}{role.Name} (HP: {role.Hp}, MP: {role.Mp}, STR: {role.Str}, State: {role.State})。");
            role.HandleStartOfTurn();
            if (role.CanAction())
            {
                var s1 = role.TemplateS1();
                var s2 = role.TemplateS2(s1);
                role.S3(s1, s2);
            }
            role.HandleEndOfTurn();

            // 更新雙方軍隊狀態
            UpdateTroop(actingTroop);
            UpdateTroop(opposingTroop);

            // 檢查在該角色行動後，戰鬥是否結束
            if (Annihilate(actingTroop) || Annihilate(opposingTroop))
            {
                return true; // 戰鬥結束，回傳 true
            }
        }
    
        return false; // 該軍隊回合結束，但戰鬥尚未結束，回傳 false
    }

    public void Battle()
    {
        while (true)
        {
            // 處理軍隊1的回合，如果戰鬥結束，就跳出迴圈
            if (ProcessTurn(_t1, _t2))
            {
                break;
            }

            // 處理軍隊2的回合，如果戰鬥結束，就跳出迴圈
            if (ProcessTurn(_t2, _t1))
            {
                break;
            }
        }
        var log = Annihilate(_t1) ? "你失敗了！" : "你獲勝了！";
        Console.WriteLine(log);
    }

    private bool Annihilate(Troop troop)
    {
        return !troop.Roles.Any();
    }

    private ICommand TemplateS1(Role role)
    {
        return role.TemplateS1();
    }

    private List<Role> TemplateS2(Role role, ICommand s1)
    {
        return role.TemplateS2(s1);
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