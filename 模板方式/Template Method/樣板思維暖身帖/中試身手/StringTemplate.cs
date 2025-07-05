namespace Template_Method.樣板思維暖身帖.中試身手;

public abstract class StringTemplate<T>
{
    public T Search(String[] messages)
    {
        T result = SetDefaultValues();
        for (int i = 0; i < messages.Length; i++)
        {
            result = UpdateResult(messages[i], i);
            Console.WriteLine(messages[i]);
            if (SearchEnd(i, messages))
            {
                break;
            }
        }
        return result;
    }

    protected abstract T UpdateResult(string messages, int index);

    protected abstract T? SetDefaultValues();

    protected virtual bool SearchEnd(int index, string[] messages)
    {
        return false;
    }

}
// 走訪每個字串 i=0...n-1，直到(走訪終止條件):
// 搜尋結果 = (藉由索引 i 和第 i 個字串來更新搜尋結果)
// 印出第 i 個字串
//     回傳（若無搜尋結果，則回傳預設值）