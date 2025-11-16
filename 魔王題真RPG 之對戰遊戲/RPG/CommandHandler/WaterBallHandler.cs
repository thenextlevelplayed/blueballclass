using RPG.ActionOption;
using RPG.Command;

namespace RPG.CommandHandler;

public class WaterBallHandler : AbstractCommandHandler
{
    protected override bool CanHandle(string skillName)
    {
        return skillName == "水球";
    }

    protected override void Process(Role role)
    {
        var waterBall = new WaterBall(role);
        role.Commands.Add(new WaterBallCommand(waterBall));
    }
}