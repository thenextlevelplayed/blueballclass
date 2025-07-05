namespace Template_Method.樣板思維暖身帖.中試身手;

public class SearchEmptyMessageIndex : StringTemplate<int>
{
    // public int Search(String[] messages) {
    //     int index = 0;
    //     while (index < messages.Length && !String.IsNullOrEmpty(messages[index])) {
    //         Console.WriteLine(messages[index]);
    //         index ++;
    //     }
    //     return index >= messages.Length ? -1 : index;
    // }

    private int index; // 追蹤當前索引

    protected override int SetDefaultValues()
    {
        index = 0; // 初始化索引
        return -1; // 預設值為 -1，表示未找到空字串
    }

    protected override int UpdateResult(string message, int i)
    {
        index = i;
        return index = String.IsNullOrEmpty(message) ? i : -1;
    }

    protected override bool SearchEnd(int i, string[] messages)
    {
        return String.IsNullOrEmpty(messages[i]); // 遇到空字串時終止
    }
}

// 變動之處以小括弧標注出來：
// 走訪每個字串 i=0...n-1，直到(遇到空字串):
// (更新空字串索引)
//     印出第 i 個字串
// 回傳(空訊息索引，預設為 null)