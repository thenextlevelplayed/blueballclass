namespace Template_Method.樣板思維暖身帖;

public abstract class Template
{
    public void TemplateMethod(int[] u)
    {
        int n = u.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (CompareMethod(u,i,j))
                {
                    int mak = u[j];
                    u[j] = u[j + 1];
                    u[j + 1] = mak;
                }
            }
        }
    }
    
    protected abstract bool CompareMethod(int[] u,int i,int j);
}