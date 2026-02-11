namespace FSM;

// 建議加上泛型 <T>，這樣這個引擎未來才能給別的專案用 (符合架構圖的設計)
// T 就是 IWaterballBotContext
public class TriggerContext<T>
{
    public InputEvent InputEvent { get; } // 建議唯讀
    public T Context { get; }             // 建議唯讀
    public LifecycleSignal Signal { get; } // 建議唯讀

    public TriggerContext(InputEvent inputEvent, T context, LifecycleSignal signal = LifecycleSignal.None)
    {
        InputEvent = inputEvent;
        Context = context;
        Signal = signal;
    }

    // ✨ 關鍵修正：回傳新的實例 (Clone with new signal)
    public TriggerContext<T> WithSignal(LifecycleSignal signal)
    {
        return new TriggerContext<T>(this.InputEvent, this.Context, signal);
    }
    
    // ⚠️ 移除了 IsMentioningBot 和 ContentEquals
    // 這些等到 Phase 2 我們寫 "Guard" 或 "Trigger" 的時候再寫在外面
}