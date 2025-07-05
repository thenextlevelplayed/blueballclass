using 二維地圖冒險遊戲.CharacterObject;
using 二維地圖冒險遊戲.Enum;

namespace 二維地圖冒險遊戲.CharacterState;

public class Erupting(Character character) : State(character)
{
    public override int InitialDuration { get; } = 3;
    
    public override void EntryState()
    {
        var actionDetails = new ActionDetails
        {
            BaseDamage = 50,
            RangeType = Character is Main ? AttackRangeType.FullMapArea : AttackRangeType.OnePointMapArea,
            AllowDirection =  new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right },
        };
        character.ActionDetails = actionDetails;
    }


    public override void Attack(Map map, ActionDetails actionDetails)
    {
        List<Character> targets = new List<Character>();
        if (Character is Main)
        {
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    // 先把所有怪物收集起來
                    if (map.Grid[x, y] is Monster monster)
                    {
                        targets.Add(monster);
                    }
                }
            }
        }
        else if (Character is Monster) // 如果怪物也有爆發狀態
        {
            for (int w = 0; w < map.Width; w++)
            {
                for (int h = 0; h < map.Height; h++)
                {
                    if (map.Grid[w, h] is Main main)
                    {
                        targets.Add(main);
                    }
                }
            }
        }
    
        // 遍歷目標列表，發動攻擊
        foreach (var target in targets)
        {
            // 檢查目標是否在攻擊過程中已經死亡（被其他效果波及）
            if (target.Hp > 0) 
            {
                target.UnderAttack(Character.ActionDetails.BaseDamage, map);
            }
        }
    }

    public override void HandleUnderAttack(int attackerAp, Map map)
    {
        Character.ApplyDamage(attackerAp, map);
        Character.EnterState(new Invincible(Character));
    }

    public override void HandleStartOfTurn()
    {
        
    }
    
    public override void HandleEndOfTurn(Map map) //回合結束動作
    {
        if (Character.Duration > 0)
        {
            Character.Duration--;
        }
        
        if (Character.Duration == 0)
        {
            Character.EnterState(new Teleport(Character));
        }
    }
    
    public override bool IsAttackConditionMet(Map map, Character character)
    {
        return true;
    }
}