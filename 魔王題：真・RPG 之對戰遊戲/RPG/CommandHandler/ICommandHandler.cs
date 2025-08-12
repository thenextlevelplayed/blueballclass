namespace RPG.CommandHandler;

public interface ICommandHandler
{
    ICommandHandler SetNext(ICommandHandler handler);
    void Handle(string skillName, Role role);
}