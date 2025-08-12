namespace RPG.ActionOption;

public interface IOnePunchCommandHandler
{
    IOnePunchCommandHandler SetNext(IOnePunchCommandHandler handler);
    void Handle(Role role);
}