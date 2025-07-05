namespace Template_Method.樣板思維暖身帖.中試身手;

public class SearchLongestMessage : StringTemplate<string>
{
    // public String Search(String[] messages) {
    //     String maxLengthMessage = "";
    //     foreach (var message in messages)
    //     {
    //         if (message.Length > maxLengthMessage.Length) {
    //             maxLengthMessage = message;
    //         }
    //         Console.WriteLine(message);
    //     }
    //    
    //     return maxLengthMessage;
    // }
    private String maxLengthMessage = "";

    protected override string SetDefaultValues()
    {
        maxLengthMessage = "";
        return "";
    }

    protected override string UpdateResult(string message, int index)
    {
        return maxLengthMessage =
            message.Length > maxLengthMessage.Length ? message : maxLengthMessage;
    }
}
// 變動之處以小括弧標注出來：
// 走訪每個字串 i=0...n-1，直到(走訪完所有字串):
// (更新最大長度字串)
//     印出第 i 個字串
// 回傳(最大長度字串，預設為 null)