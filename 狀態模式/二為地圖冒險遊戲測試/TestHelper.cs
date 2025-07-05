namespace 二為地圖冒險遊戲測試;
using 二維地圖冒險遊戲;
using 二維地圖冒險遊戲.CharacterObject;
using 二維地圖冒險遊戲.CharacterState;
using 二維地圖冒險遊戲.Enum;
// ...等等
[TestClass]

    
public static class TestHelper
{
    // 建立一個包含 Main 角色的乾淨遊戲環境
    public static (Map map, Main main) CreateTestEnvironment(int mapWidth = 5, int mapHeight = 5)
    {
        var map = new Map(mapWidth, mapHeight);
        var main = new Main(Direction.Right);
        map.PlaceObject(main, 2, 2); // 將主角固定放在地圖中央
        return (map, main);
    }

    // 模擬一個完整的遊戲回合（玩家動作 + 怪物回合 + 回合結束處理）
    // action 是一個委派，代表玩家在本回合要執行的操作
    public static void SimulateRound(Map map, Main main, Action<Map, Main> playerAction)
    {
        // 1. 回合開始
        main.HandleStartOfTurn();

        // 2. 執行玩家傳入的動作
        playerAction(map, main);

        // 3. 模擬怪物回合 (為簡單起見，這裡先不放怪物，專注測試主角狀態)
        // ...

        // 4. 回合結束
        main.HandleEndOfTurn(map);
    }
}

