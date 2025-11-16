namespace RPG.ActionOption;

public abstract class AbstractOnePunchCommandHandler(OnePunch skill) : IOnePunchCommandHandler
{
    private IOnePunchCommandHandler? NextHandler;
    protected readonly OnePunch Skill = skill;

    public IOnePunchCommandHandler SetNext(IOnePunchCommandHandler handler)
    {
        NextHandler = handler;
        return handler;
    }

    public void Handle(Role role)
    {
        if (CanHandle(role))
        {
            Process(role);
        }
        else
        {
            NextHandler?.Handle(role);
        }
    }

    protected abstract bool CanHandle(Role role);

    protected abstract void Process(Role role);

    public virtual int Str => 100;
}