namespace FSM;

public class FsmResult
{
    public bool Handled { get; private set; }
    public bool StateChanged { get; private set; }

    // 私有建構子，強制使用下方的靜態方法建立
    private FsmResult(bool handled, bool stateChanged)
    {
        Handled = handled;
        StateChanged = stateChanged;
    }

    // 1. 沒人處理這個事件 (Bubble up)
    public static FsmResult Ignored() 
    {
        return new FsmResult(false, false);
    }

    // 2. 處理了，但是是內部轉移 (不換狀態) -> ✨ 修正：Handled 應為 true
    public static FsmResult Internal()
    {
        return new FsmResult(true, false);
    }

    // 3. 處理了，且切換了狀態 (Consumed)
    public static FsmResult External()
    {
        return new FsmResult(true, true);
    }
    
    public static FsmResult Merge(FsmResult a, FsmResult b)
    {
        return new FsmResult(
            a.Handled || b.Handled, 
            a.StateChanged || b.StateChanged
        );
    }
}