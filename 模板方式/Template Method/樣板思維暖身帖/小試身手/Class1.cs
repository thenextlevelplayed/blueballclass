namespace Template_Method.樣板思維暖身帖.小試身手;

public class Class1:Template
{
    // public void P1(int[] u)
    // {
    //     int n = u.Length;
    //     for (int i = 0; i < n - 1; i++)
    //     {
    //         for (int j = 0; j < n - i - 1; j++)
    //         {
    //             if (u[j] > u[j + 1])//相異之處
    //             {
    //                 int mak = u[j];
    //                 u[j] = u[j + 1];
    //                 u[j + 1] = mak;
    //             }
    //         }
    //     }
    protected override  bool CompareMethod(int i,int j)
    {
        return i<j;
    }
}