namespace RPG;

public class AI : Role
{
    private int Seed { get; set; } = 0;

    public AI(string name, int hp, int mp, int str) : base(name, hp, mp, str)
    {
    }
    
    protected override int SelectActionIndexHook()
    {
        // 根據目前的 seed 決定要嘗試的行動
        int currentIndex = this.Seed % Commands.Count;

        // AI 的規則是：不論這次嘗試成功或失敗，下次都要試下一個
        // 所以在這裡就直接更新 Seed，為下一次決策做準備
        this.Seed++;
        
        return currentIndex;
    }

    protected override void S2PostDecisionHook()
    {
        // this.Seed++;
    }

    protected override List<Role> SelectTargetsHook(List<Role> availableTargets, int targetConditionMaxTargets)
    {
        var selectedIndices = new List<int>();
        for (int i = 0; i < targetConditionMaxTargets; i++)
        {
            // 在搜尋過程中，只使用 Seed 來計算
            int numb = (this.Seed + i) % availableTargets.Count();
            selectedIndices.Add(numb);
        }

        S2PostDecisionHook();
        return availableTargets.Where((_, index) => selectedIndices.Distinct().Contains(index)).ToList();
    }
}