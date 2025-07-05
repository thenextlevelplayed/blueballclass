using 二維地圖冒險遊戲.CharacterObject;
using 二維地圖冒險遊戲.Enum;

namespace 二維地圖冒險遊戲.CharacterState;

public abstract class State
{
    public Character Character { get; set; }

    public State(Character character)
    {
        Character = character;
    }

    public virtual int InitialDuration { get; }

    public virtual int GetNumberOfActionsPerTurn => 1;

    public virtual void EntryState()
    {
    }

    public virtual void ExitState()
    {
    }

    public virtual void Attack(Map map, ActionDetails actionDetails)
    {
        if (actionDetails.RangeType is AttackRangeType.Line && Character is Main)
        {
            AttackInFacingDirection(map);
        }

        if (actionDetails.RangeType is AttackRangeType.OnePointMapArea && Character is Monster)
        {
            int currentX = Character.X.Value; // 假設 X, Y 此時必有值
            int currentY = Character.Y.Value;

            int[] dx = { 0, 0, -1, 1 }; // X方向的變化量
            int[] dy = { -1, 1, 0, 0 }; // Y方向的變化量 (假設Y向下為正，向上為負)
            for (int i = 0; i < dx.Length; i++)
            {
                int neighborX = currentX + dx[i];
                int neighborY = currentY + dy[i];
                if (neighborX >= 0 && neighborX < map.Width &&
                    neighborY >= 0 && neighborY < map.Height)
                {
                    IMapObject obj = map.GetObjectAt(neighborX, neighborY);
                    if (obj is Main main)
                    {
                        main.UnderAttack(Character.Ap, map);
                        Console.WriteLine($"在 ({neighborX},{neighborY}) 發現玩家: {Character.DisplaySymbol}. 採取攻擊行動...");
                    }
                }
            }
        }
    }

    public abstract void HandleUnderAttack(int attackerAp, Map map);

    public virtual bool CanPerformAttack()
    {
        return true;
    }

    public abstract void HandleStartOfTurn(); //回合開始動作

    public virtual void HandleEndOfTurn(Map map) //回合結束動作
    {
        if (Character.Duration > 0)
        {
            Character.Duration--;
        }
        
        if (Character.Duration == 0)
        {
            Character.EnterState(new Normal(Character));
        }
       
    }

    private void AttackInFacingDirection(Map map)
    {
        int dx = 0; // X方向的步進
        int dy = 0; // Y方向的步進

        // 根據面向的方向設定步進值
        switch (Character.FaceInDirection)
        {
            case Direction.Right:
                dx = 1;
                break;
            case Direction.Left:
                dx = -1;
                break;
            case Direction.Up:
                dy = -1; // 通常Y軸向上是減少
                break;
            case Direction.Down:
                dy = 1; // 通常Y軸向下是增加
                break;
            default:
                // 如果有未知的方向，可以選擇拋出錯誤或直接返回
                // Console.WriteLine($"Unhandled direction: {this.FaceInDirection}");
                return;
        }

        // 如果dx和dy都為0 (例如，從default情況或者一個未處理/中立的方向)，
        // 下面的迴圈不會執行，這是正確的。
        int currentX = Character.X.Value;
        int currentY = Character.Y.Value;

        while (true)
        {
            // 移動到下一個格子
            currentX += dx;
            currentY += dy;

            // 檢查是否超出地圖邊界
            if (currentX < 0 || currentX >= map.Width ||
                currentY < 0 || currentY >= map.Height)
            {
                break; // 到達地圖邊緣，停止攻擊
            }

            object target = map.Grid[currentX, currentY];

            if (target is Monster monster)
            {
                monster.UnderAttack(Character.Ap, map);
                // 根據遊戲規則，攻擊後是否停止？
                // 原始程式碼中，攻擊怪物後會繼續檢查該方向上的下一個格子，所以這裡不 break
            }
            else if (target is Obstacle obstacle)
            {
                // 遇到障礙物，停止攻擊
                break;
            }
            // 如果是空格或其他非怪物/非障礙物，則繼續檢查下一個格子
        }
    }
    
    public virtual bool IsAttackConditionMet(Map map, Character character)
    {
        // 只有怪物需要檢查這個，主角的攻擊是主動的
        if (character is Monster)
        {
            // 複用你已有的 FindAdjacentPlayer 方法
            var adjacentPlayer = Game.FindAdjacentPlayer(map, character.X.Value, character.Y.Value);
            return adjacentPlayer != null;
        }
        // 主角或其他角色類型預設為 false，因為他們的攻擊由玩家觸發
        return false; 
    }
}