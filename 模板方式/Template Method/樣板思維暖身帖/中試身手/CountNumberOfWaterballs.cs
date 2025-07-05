namespace Template_Method.樣板思維暖身帖.中試身手;

public class CountNumberOfWaterballs : StringTemplate<int>
{
    // public int Count(String[] messages) {
    //     int count = 0;
    //     foreach (var message in messages)
    //     {
    //         if ("Waterball".Equals(message)) {
    //             count ++;
    //         }
    //         Console.WriteLine(message);
    //     }
    //     
    //     return count;
    // }

    private int Count;

    protected override int SetDefaultValues()
    {
        Count = 0;
        return 0;
    }

    protected override int UpdateResult(string message, int index)
    {
        if ("Waterball".Equals(message))
        {
            Count++;
        }
        return Count; // 回傳更新後的計數
    }
}
// 變動之處以小括弧標注出來：
// 走訪每個字串 i=0...n-1，直到(走訪完所有字串):
// (更新 'Waterball' 字串數量)
// 印出第 i 個字串
// 回傳('Waterball' 字串數量，預設為 0)