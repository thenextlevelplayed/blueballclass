namespace RPG;

public class Troop
{
    public Rpg Rpg { get; set; }
    private TroopRelation TroopRelation { get; set; }
    public Troop EnemyTroop { get; private set; }
    public List<Role> Roles { get; set; }

    public Troop(List<Role> roles)
    {
        Roles = roles;
        TroopRelation = new TroopRelation();
        foreach (var role in roles)
        {
            role.Troop = this;
        }
    }

    public static void SetRelation(Troop allyTroop, Troop enemyTroop)
    {
        // 設定 allyTroop 的敵人是 enemyTroop
        allyTroop.EnemyTroop = enemyTroop;
        allyTroop.TroopRelation.AllyTroop = allyTroop;
        allyTroop.TroopRelation.EnemyTroop = enemyTroop;
        // 設定 enemyTroop 的敵人是 allyTroop
        enemyTroop.EnemyTroop = allyTroop;
        enemyTroop.TroopRelation.AllyTroop = enemyTroop;
        enemyTroop.TroopRelation.EnemyTroop = allyTroop;
    }

    public override string ToString()
    {
        var getHero = Roles.FirstOrDefault(m => m is Hero);
        return getHero == null ? "[2]" : "[1]";
    }
}