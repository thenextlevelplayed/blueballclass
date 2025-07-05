using 二維地圖冒險遊戲.CharacterState;
using 二維地圖冒險遊戲.Enum;
using 二維地圖冒險遊戲.TreasureObject;

namespace 二維地圖冒險遊戲.CharacterObject;

public abstract class Character : IMapObject
{
    public State State { get; set; }
    public virtual int Hp { get; set; }
    public virtual int Ap { get; set; }
    public int? X { get; set; } = null;
    public int? Y { get; set; } = null;
    public int Duration { get; set; }
    public virtual int MaxHp { get; set; }
    public Direction FaceInDirection { get; set; }
    public string DisplaySymbol { get; set; }
    
    public Random Random = new Random();
    public ActionDetails ActionDetails { get; set; }
    private bool _justEnteredState = false; 
    public Character(Direction direction)
    {
        State = new Normal(this);
        FaceInDirection = Direction.Right;
        DisplaySymbol = GetSymbolString();
        ActionDetails = new ActionDetails
        {
            BaseDamage = this.Ap,
            RangeType = AttackRangeType.Line,
            AllowDirection = new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right },
        };
    }

    public void SetPosition(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public void Attack(Map map, ActionDetails actionDetails)
    {
        this.State.Attack(map,actionDetails);
    }

    public void Move(Map map, int x, int y, Direction direction)
    {
        if (x >= 0 && y >= 0 && x < map.Width && y < map.Height && map.Grid[x, y] is null)
        {
            map.RemoveObjectAt(this.X.Value, this.Y.Value);
            map.PlaceObject(this, x, y);
            this.FaceInDirection = direction;
            Console.WriteLine($"{this.DisplaySymbol} 往座標X:{X.Value} 座標Y:{Y.Value}移動。");
        }
        else if (x >= map.Width || y >= map.Height || x < 0 || y < 0)
        {
            Console.WriteLine("你已超出地圖範圍");
        }
        else
        {
            Touch(map.Grid[x, y], map); //發生碰撞
        }

        this.FaceInDirection = direction;
        this.DisplaySymbol = GetSymbolString();
    }

    private void Touch(IMapObject mapObject, Map map)
    {
        if (mapObject is Treasure treasure)
        {
            Console.WriteLine($"{this.DisplaySymbol}與寶藏發生碰撞:{treasure.GetType().Name}");
            var state = treasure.Touched(this);
            this.EnterState(state);
            map.RemoveObjectAt(treasure.X.Value, treasure.Y.Value);
        }
        else
        {
            Console.WriteLine("與怪物或障礙物發生碰撞，留在原地罰站。");
        }
    }

    public void HandleStartOfTurn()
    {
        State.HandleStartOfTurn();
    }

    public void HandleEndOfTurn(Map map)
    {
        if (_justEnteredState)
        {
            _justEnteredState = false;
            return;
        }
        State.HandleEndOfTurn(map);
    }

    public virtual void UnderAttack(int attackerAp, Map map)
    {
        Console.WriteLine($"{this.DisplaySymbol} ({this.State.GetType().Name}) 準備受到攻擊，攻擊力: {attackerAp}");
        State.HandleUnderAttack(attackerAp, map);
    }

    protected internal void ApplyDamage(int attackAp, Map map)
    {
        this.Hp = this.Hp - attackAp;
        CheckHp(map);
    }

    public void EnterState(State state)
    {
        Console.WriteLine($"DEBUG: EnterState called. New state: {state.GetType().Name}");
        this.State.ExitState();
        this.State = state;
        this.Duration = State.InitialDuration;
        state.EntryState();
        _justEnteredState = true;
    }

    protected string GetSymbolString()
    {
        if (this.GetType() == typeof(Main))
        {
            if (FaceInDirection == Direction.Right)
            {
                return "→";
            }

            if (FaceInDirection == Direction.Left)
            {
                return "←";
            }

            if (FaceInDirection == Direction.Up)
            {
                return "↑";
            }

            if (FaceInDirection == Direction.Down)
            {
                return "↓";
            }
        }

        if (this.GetType() == typeof(Monster))
        {
            return "M";
        }

        if (this.GetType() == typeof(Obstacle))
        {
            return "□";
        }

        return "unknown";
    }

    private void CheckHp(Map map)
    {
        if (Hp > MaxHp)
        {
            Hp = MaxHp;
        }

        if (Hp <= 0)
        {
            map.RemoveObjectAt(this.X.Value, this.Y.Value);
            Console.WriteLine(this.DisplaySymbol + "HP小於0，已死亡。");
        }
    }

    public virtual bool CanAttack()
    {
        return State.CanPerformAttack();
    }
}