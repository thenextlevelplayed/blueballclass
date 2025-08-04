namespace RPG;

public class Troop
{
    public Rpg Rpg { get; set; }
    private TroopRelation TroopRelation { get; set; }
    private Troop EnemyTroop { get; set; }
    public List<Role> Roles { get; set; }
    
    public Troop(Troop enemyTroop, List<Role> roles)
    {
        var troopRelation = new TroopRelation();
        troopRelation.EnemyTroop = enemyTroop;
        troopRelation.AllyTroop = this;
        TroopRelation = troopRelation;
        Roles = roles;
    }
}