namespace RPG;

public class Hero : Role
{
    public Hero(string name, int hp, int mp, int str) : base(name, hp, mp, str)
    {
    }
    protected override int SelectActionIndexHook()
    {
        // 不斷讀取輸入，直到玩家輸入一個有效的數字格式
        while (true)
        {
            var input = Troop.Rpg.ReadInput();
            if (int.TryParse(input, out int index))
            {
                return index; // 格式正確，返回索引讓父類別的 S1 去驗證
            }
            // 如果輸入的不是數字，父類別的 S1 會處理提示訊息
        }
    }

    protected override void S2PostDecisionHook()
    {
    }

    protected override List<Role> SelectTargetsHook(List<Role> availableTargets, int targetConditionMaxTargets)
    {
        // 如果目標數量小於最大目標數量，則選擇目標
        string output = $"選擇 {targetConditionMaxTargets} 位目標: ";
        for (int i = 0; i < availableTargets.Count(); i++)
        {
            output += $"({i}) {availableTargets[i].Troop}{availableTargets[i].Name} ";
        }

        Console.WriteLine(output.Trim());
        List<int> selectedIndices = new List<int>();
        while (true)
        {
            var input = Troop.Rpg.ReadInput();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("請輸入有效的目標索引。");
                continue; // 繼續下一次迴圈
            }

            // 3. 加入 try-catch 處理非數字輸入
            try
            {
                List<int> tempIndices = input.Split(',')
                    .Select(s => int.Parse(s.Trim())) // 使用 Trim() 增加彈性
                    .Distinct() // 使用 Distinct() 避免玩家輸入重複的索引
                    .ToList();

                if (tempIndices.Count == targetConditionMaxTargets)
                {
                    // (可選的優化) 檢查索引是否都在合法範圍內
                    if (tempIndices.All(i => i >= 0 && i < availableTargets.Count))
                    {
                        selectedIndices = tempIndices;
                        break; 
                    }
                }
                
                Console.WriteLine($"輸入無效，請選擇 {targetConditionMaxTargets} 個不重複的有效目標索引。");
            }
            catch (FormatException)
            {
                Console.WriteLine("輸入格式錯誤，請輸入以逗號分隔的數字。");
            }
        }

        return availableTargets.Where((_, index) => selectedIndices.Contains(index)).ToList();
    }
}