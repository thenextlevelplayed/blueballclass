using System.Text;
using 二維地圖冒險遊戲.CharacterObject;
using 二維地圖冒險遊戲.TreasureObject;

namespace 二維地圖冒險遊戲;

public class Map
{
    public IMapObject[,] Grid { get; set; } // <--- 型別改變
    public int Width { get; private set; } // 建議設為 private set，由建構子初始化
    public int Height { get; private set; } // 建議設為 private set

    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        Grid = new IMapObject[width, height]; // <--- 初始化型別改變
        // 預設情況下，Grid 中的所有元素都會是 null
    }

    // 放置物件到地圖上
    public bool PlaceObject(IMapObject obj, int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            Grid[x, y] = obj;
            if (obj is Character mapObj) // 檢查是否可以設定位置
            {
                mapObj.SetPosition(x, y); // 或者 mapObj.X = x; mapObj.Y = y;
            }

            if (obj is Treasure treasure) // 檢查是否可以設定位置
            {
                treasure.SetPosition(x, y); // 或者 mapObj.X = x; mapObj.Y = y;
            }

            if (obj is Obstacle obstacle)
            {
                obstacle.SetPosition(x, y);
            }

            return true;
        }

        return false; // 位置超出邊界
    }

    // 從地圖上取得物件
    public IMapObject GetObjectAt(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            return Grid[x, y];
        }

        return null; // 位置超出邊界或該位置沒有物件
    }

    // 移除地圖上的物件
    public IMapObject RemoveObjectAt(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            IMapObject obj = Grid[x, y];
            Grid[x, y] = null; // 設為 null 代表空
            Console.WriteLine($"{obj.DisplaySymbol} 從地圖移除。");
            return obj;
        }

        return null;
    }

    // 簡單的顯示地圖方法 (可選)
    public void DisplayMap()
    {
        StringBuilder sb = new StringBuilder();
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (Grid[x, y] == null)
                {
                    sb.Append('.'); // 空地用 '.' 表示
                }
                else
                {
                    sb.Append(Grid[x, y].DisplaySymbol);
                }

                sb.Append(' '); // 為了美觀加個空格
            }

            sb.AppendLine();
        }

        Console.WriteLine(sb.ToString());
    }
}