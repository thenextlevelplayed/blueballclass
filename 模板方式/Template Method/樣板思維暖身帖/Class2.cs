namespace Template_Method.樣板思維暖身帖;

public class Class2
{
    public void P2(int[] k)
    {
        int n = k.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (k[j] < k[j + 1]) //相異之處
                {
                    int ppp = k[j];
                    k[j] = k[j + 1];
                    k[j + 1] = ppp;
                }
            }
        }
    }
}