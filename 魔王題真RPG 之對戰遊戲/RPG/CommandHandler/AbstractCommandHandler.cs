namespace RPG.CommandHandler;

public abstract class AbstractCommandHandler : ICommandHandler
{
    private ICommandHandler? NextHandler;

    public ICommandHandler SetNext(ICommandHandler handler)
    {
        this.NextHandler = handler;
        return handler;
    }

    public void Handle(string skillName, Role role)
    {
        if (CanHandle(skillName))
        {
            Process(role);
        }

        else if (this.NextHandler != null)
        {
            this.NextHandler.Handle(skillName, role);
        }
    }

    protected abstract bool CanHandle(string skillName);

    protected abstract void Process(Role role);
}