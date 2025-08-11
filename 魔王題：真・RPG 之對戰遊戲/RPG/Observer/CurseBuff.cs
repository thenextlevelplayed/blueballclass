namespace RPG.Observer;

public class CurseBuff(Role role) : IObserver
{
    public Role Role { get; set; } = role;

    public void Action()
    {
        foreach (var curse in Role.TheCursed)
        {
            if (curse.Caster.Hp > 0)
            {
                curse.Caster.Hp += curse.Victim.Mp;
                curse.Caster.Spellcaster.Remove(curse);
            }
        }
        
    }
}