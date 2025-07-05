namespace Template_Method.樣板思維暖身帖;

public class Asc:Template
{
    protected override  bool CompareMethod(int[] u,int i,int j)
    {
        return u[j] < u[j + 1];
    }
}