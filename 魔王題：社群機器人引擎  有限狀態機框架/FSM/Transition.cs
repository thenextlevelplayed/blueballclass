namespace FSM;

public class Transition<T>
{
    public Func<TriggerContext<T>, bool>? Guard { get; set; }
    public IAction<T>? Action { get; set; }
    public State<T>? To { get; set; }
    public ITrigger<T> Trigger { get; set; }
}

public interface ITrigger<T>
{
    public bool IsTriggered(TriggerContext<T> context);
}

public class EventTypeTrigger<T> : ITrigger<T>
{
    private readonly string _eventName;
    public EventTypeTrigger(string eventName) => _eventName = eventName;

    public bool IsTriggered(TriggerContext<T> context)
    {
        // 🛑 關鍵修正：
        // 如果現在是在處理 Entry 信號 (為了找自動轉移)，
        // 那麼這個 Event Trigger 應該要閉嘴，不要再回應舊的 Event 了。
        if (context.Signal == LifecycleSignal.Entry) return false;

        // 只有在一般情況下，才比對 Event Name
        return context.InputEvent.Name == _eventName;
    }
}

public class OnEntryTrigger<T> : ITrigger<T>
{
    public bool IsTriggered(TriggerContext<T> context)
    {
        // 只有在 Signal 為 Entry 時才觸發
        // 這是為了 Phase 3 的自動轉移機制鋪路
        return context.Signal == LifecycleSignal.Entry;
    }
}

public class State<T>
{
    public string Name { get; }
    public IAction<T> EntryAction { get; set; }
    public IAction<T> ExitAction { get; set; }
    public List<Transition<T>> Transitions { get; } = new List<Transition<T>>();

    public State(string name) => Name = name;

    // Helper: 方便加入轉移
    public void AddTransition(Transition<T> transition)
    {
        Transitions.Add(transition);
    }

    public void Entry(TriggerContext<T> ctx)
    {
        EntryAction?.Execute(ctx);
    }

    public void Exit(TriggerContext<T> ctx)
    {
        ExitAction?.Execute(ctx);
    }
}

public interface IAction<T>
{
    public void Execute(TriggerContext<T> context);
}

public class FiniteStateMachine<T> : IStateMachine<T>
{
    private const int MAX_ROUNDS = 10;
    public State<T> InitialState { get; set; }
    public State<T> CurrentState { get; set; }

    private void Entry(Transition<T> t, TriggerContext<T> ctx)
    {
        if (t.To != null)
        {
            // set new state
            CurrentState = t.To;
            var entryCtx = ctx.WithSignal(LifecycleSignal.Entry);
            // call Entry on the new state
            CurrentState.Entry(entryCtx);
            // Note: if CurrentState is SubMachineState, its Entry will call internalMachine.Start(entryCtx)
        }
    }

    private void ExecuteAction(Transition<T> t, TriggerContext<T> ctx)
    {
        if (t.Action != null)
        {
            t.Action.Execute(ctx); // Action 需要完整的 ctx (包含 Event)
        }
    }

    private void Exit(Transition<T> t, TriggerContext<T> ctx)
    {
        // 只有當真的要離開狀態時才執行 Exit (避免 Internal Transition 誤觸發)
        if (t.To != null)
        {
            CurrentState.Exit(ctx); // 👈 關鍵：傳 Context 給 State
        }
    }

    public FsmResult ExecuteTransition(Transition<T> t, TriggerContext<T> ctx)
    {
        // guard: ensure t.From == CurrentState (stale check)
        if (t.To != null)
        {
            // external
            Exit(t, ctx);
            ExecuteAction(t, ctx);
            Entry(t, ctx); // sets CurrentState = t.To, calls new state's Entry(ctx)
            return FsmResult.External();
        }
        else
        {
            // internal transition (no state change)
            ExecuteAction(t, ctx);
            return FsmResult.Internal();
        }
    }

    // public FsmResult Fire(InputEvent e)
    // {
    //     var ctx = new TriggerContext(e, new T(), LifecycleSignal.None);
    //     return Handle(ctx);
    // }

    private FsmResult ResolveAutomaticTransitions(TriggerContext<T> context)
    {
        var ctx = context.WithSignal(LifecycleSignal.Entry);
        var accumulated = FsmResult.Ignored();
        for (int i = 0; i < MAX_ROUNDS; i++)
        {
            var t = LookupTransition(ctx); // matches only OnEntry/eventless triggers when signal=Entry
            if (t == null) break;
            var res = ExecuteTransition(t, ctx);
            accumulated = FsmResult.Merge(accumulated, res);
        }

        return accumulated;
    }

    public FsmResult Handle(TriggerContext<T> ctx)
    {
        var resultTotal = FsmResult.Ignored();

        var t = LookupTransition(ctx);
        if (t == null) return resultTotal;

        var res = ExecuteTransition(t, ctx);
        var auto = ResolveAutomaticTransitions(ctx);
        return FsmResult.Merge(res, auto);
    }

    public Transition<T> LookupTransition(TriggerContext<T> ctx)
    {
        return CurrentState.Transitions.FirstOrDefault(m =>
            m.Trigger.IsTriggered(ctx) && (m.Guard == null || m.Guard(ctx)));
    }

    public FsmResult Start(TriggerContext<T> entryCtx)
    {
        // 保證 signal = Entry
        var ctx = entryCtx.WithSignal(LifecycleSignal.Entry);

        CurrentState = InitialState;
        // 呼 CurrentState.Entry(ctx) —— State.Entry 接受 TriggerContext
        CurrentState.Entry(ctx);
        // 處理 OnEntry / eventless transitions（用 Entry signal）
        var autoResult = ResolveAutomaticTransitions(ctx);
        return autoResult; // 可能包含 Handled/StateChanged
    }

    public FsmResult Stop(TriggerContext<T> exitCtx)
    {
        var ctx = exitCtx.WithSignal(LifecycleSignal.Exit);
        CurrentState?.Exit(ctx);
        // optional: stop child submachines if CurrentState is SubMachineState
        return FsmResult.External();
    }
}

public interface IStateMachine<T>
{
    public State<T> InitialState { get; set; }
    public State<T> CurrentState { get; set; }
    public FsmResult Handle(TriggerContext<T> ctx);
    public FsmResult Start(TriggerContext<T> entryCtx);
    public FsmResult Stop(TriggerContext<T> exitCtx);
}