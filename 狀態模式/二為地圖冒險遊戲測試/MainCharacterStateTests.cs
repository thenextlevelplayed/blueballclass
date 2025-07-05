namespace 二為地圖冒險遊戲測試;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using 二維地圖冒險遊戲;
using 二維地圖冒險遊戲.CharacterObject;
using 二維地圖冒險遊戲.CharacterState;
using 二維地圖冒險遊戲.Enum;
using 二維地圖冒險遊戲.TreasureObject;

[TestClass]
public class MainCharacterStateTests
{
    [TestMethod]
    #region Poisoned State Test (中毒狀態測試)
    public void PoisonedState_LosesHpEachTurn_AndLastsFor3Turns()
    {
        // --- Arrange (準備) ---
        var (map, main) = TestHelper.CreateTestEnvironment();
        var initialHp = main.Hp;
        // ... Arrange ...
        main.EnterState(new Poisoned(main));
        // T0 (豁免)
        TestHelper.SimulateRound(map, main, (m, p) => { });
        // T1 (計時1)
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.AreEqual(2, main.Duration, "T1-End: Duration should be 2");
        // T2 (計時2)
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.AreEqual(1, main.Duration, "T2-End: Duration should be 1");

        // --- T3 (計時3，結束時轉換) ---
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.IsInstanceOfType(main.State, typeof(Normal), "T3-End: State should be Normal");
        Assert.AreEqual(1, main.Duration, "T3-End: Duration should be reset to Normal's duration(1)");
    }
    #endregion

    #region Invincible State Test (無敵狀態測試)
    [TestMethod]
    public void InvincibleState_TakesNoDamage_AndLastsFor2Turns()
    {
        // --- Arrange ---
        var (map, main) = TestHelper.CreateTestEnvironment();
        var monster = new Monster(Direction.Left);

        // ... Arrange ...
        main.EnterState(new Invincible(main));
        // T0 (豁免)
        TestHelper.SimulateRound(map, main, (m, p) => { });
        // T1 (第一個計時)
        TestHelper.SimulateRound(map, main, (m, p) => p.UnderAttack(monster.Ap, m));
        Assert.AreEqual(1, main.Duration, "T1-End: Duration should be 1");
        Assert.IsInstanceOfType(main.State, typeof(Invincible));

        // --- T2 (第二個計時，結束時轉換) ---
        TestHelper.SimulateRound(map, main, (m, p) => p.UnderAttack(monster.Ap, m));
        // 在T2結束時，Duration變0，觸發EnterState(Normal)，Duration又變為1
        Assert.IsInstanceOfType(main.State, typeof(Normal), "T2-End: State should be Normal");
        Assert.AreEqual(1, main.Duration, "T2-End: Duration should be reset to Normal's duration(1)");
    }
    #endregion

    #region Accelerated State Test (加速狀態測試)
    [TestMethod]
    public void AcceleratedState_GetsTwoActions_AndLosesStateWhenAttacked()
    {
        // --- Arrange ---
        var (map, main) = TestHelper.CreateTestEnvironment();
        main.EnterState(new Accelerated(main));

        // --- Act & Assert: 驗證兩次行動 ---
        Assert.AreEqual(2, main.State.GetNumberOfActionsPerTurn, "加速狀態下每回合應有 2 次行動");
        Assert.AreEqual(3, main.Duration);

        // --- Act & Assert: 驗證受攻擊後狀態消失 ---
        var monster = new Monster(Direction.Left);
        main.UnderAttack(monster.Ap, map);
        Assert.IsInstanceOfType(main.State, typeof(Normal), "加速狀態受攻擊後應立即變回正常狀態");
    }
    #endregion

    #region Healing State Test (恢復狀態測試)
    [TestMethod]
    public void HealingState_HealsHpEachTurn_AndStopsWhenFull()
    {
        // --- Arrange ---
        var (map, main) = TestHelper.CreateTestEnvironment();
        main.Hp = 100; // 先讓主角受傷
        var initialHp = main.Hp;

        // --- Act & Assert for T0 (進入狀態 & 豁免回合) ---
        main.EnterState(new Healing(main));
        Assert.AreEqual(5, main.Duration, "T0-Start: Healing duration should be 5");

        TestHelper.SimulateRound(map, main, (m, p) => { });
        // 在T0的HandleStartOfTurn中，HP就已經補了
        Assert.AreEqual(initialHp + 30, main.Hp, "T0: Entering state should heal 30 HP at start of turn");
        Assert.AreEqual(5, main.Duration, "T0-End: Duration should not decrease in exempt turn");

        // --- Act & Assert for T1 (第一個計時回合) ---
        var hpAfterT0 = main.Hp;
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.AreEqual(hpAfterT0 + 30, main.Hp, "T1: Should heal another 30 HP");
        Assert.AreEqual(4, main.Duration, "T1-End: Duration should be 4");

        // --- Act & Assert for premature end (測試補到滿血會提前結束) ---
        // 重新設定一個臨界場景
        main.Hp = 290;
        main.EnterState(new Healing(main)); // 重新進入狀態以重置計時

        // 豁免回合 (T0 for this new scenario)
        // 在這個回合的 HandleStartOfTurn，HP 就會補滿並轉換狀態
        TestHelper.SimulateRound(map, main, (m, p) => { });

        Assert.AreEqual(main.MaxHp, main.Hp, "HP should be capped at MaxHp");
        Assert.IsInstanceOfType(main.State, typeof(Normal), "State should become Normal immediately when HP is full");
        // 因為變回 Normal，Duration 會被重設為 1
        Assert.AreEqual(1, main.Duration, "Duration should be reset to Normal's duration");
    }
    #endregion

    #region Orderless State Test (混亂狀態測試)
    [TestMethod]
    public void OrderlessState_CannotAttack_AndMovementIsRestricted()
    {
        // --- Arrange ---
        var (map, main) = TestHelper.CreateTestEnvironment();

        // --- T0 (進入狀態 & 豁免回合) ---
        main.EnterState(new Orderless(main));
        Assert.AreEqual(3, main.Duration, "T0-Start: Orderless duration should be 3");

        // 驗證行為
        Assert.IsFalse(main.CanAttack(), "Should not be able to attack in Orderless state");
        bool canMoveVertically = main.ActionDetails.AllowDirection.Contains(Direction.Up) && main.ActionDetails.AllowDirection.Contains(Direction.Down);
        bool canMoveHorizontally = main.ActionDetails.AllowDirection.Contains(Direction.Left) && main.ActionDetails.AllowDirection.Contains(Direction.Right);
        Assert.IsTrue((canMoveVertically && !canMoveHorizontally) || (!canMoveVertically && canMoveHorizontally), "Movement should be restricted to vertical or horizontal");
        Assert.AreEqual(2, main.ActionDetails.AllowDirection.Count, "Should only have 2 allowed directions");

        // 模擬豁免回合
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.AreEqual(3, main.Duration, "T0-End: Duration should not decrease in exempt turn");
        Assert.IsInstanceOfType(main.State, typeof(Orderless));

        // --- T1 (第一個計時回合) ---
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.AreEqual(2, main.Duration, "T1-End: Duration should be 2");
        Assert.IsInstanceOfType(main.State, typeof(Orderless));

        // --- T2 (第二個計時回合) ---
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.AreEqual(1, main.Duration, "T2-End: Duration should be 1");
        Assert.IsInstanceOfType(main.State, typeof(Orderless));

        // --- T3 (第三個計時回合 & 狀態結束) ---
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.IsInstanceOfType(main.State, typeof(Normal), "T3-End: State should be Normal");
        Assert.AreEqual(1, main.Duration, "T3-End: Duration should be reset to Normal's duration(1)");
    }
    #endregion

    #region Stockpile -> Erupting -> Teleport Chain Test (蓄力->爆發->瞬身 鏈式測試)
    [TestMethod]
    public void Stockpile_TransitionsToErupting_ThenToTeleport()
    {
        // --- Arrange ---
        var (map, main) = TestHelper.CreateTestEnvironment();
        // 在地圖上放置一些怪物來測試全圖攻擊
        map.PlaceObject(new Monster(Direction.Up), 0, 0);
        map.PlaceObject(new Monster(Direction.Up), 4, 4);

        // --- T0: 進入蓄力 (豁免) ---
        main.EnterState(new Stockpile(main));
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.AreEqual(2, main.Duration, "T0-End: Stockpile duration should still be 2");

        // --- T1: 蓄力計時1 ---
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.AreEqual(1, main.Duration, "T1-End: Stockpile duration should be 1");
        Assert.IsInstanceOfType(main.State, typeof(Stockpile));

        // --- T2: 蓄力計時2, 結束時轉換為 Erupting ---
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.IsInstanceOfType(main.State, typeof(Erupting), "T2-End: Should be Erupting");
        // 轉換後，Duration 被 Erupting 的 InitialDuration(3) 重設了
        Assert.AreEqual(3, main.Duration, "T2-End: Duration should be reset to Erupting's duration(3)");

        // --- T3: 爆發 (豁免) ---
        TestHelper.SimulateRound(map, main, (m, p) => p.Attack(m, p.ActionDetails));
        Assert.AreEqual(3, main.Duration, "T3-End: Erupting duration (exempt) should still be 3");

        // --- T4: 爆發計時1 ---
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.AreEqual(2, main.Duration, "T4-End: Erupting duration should be 2");

        // --- T5: 爆發計時2 ---
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.AreEqual(1, main.Duration, "T5-End: Erupting duration should be 1");

        // --- T6: 爆發計時3, 結束時轉換為 Teleport ---
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.IsInstanceOfType(main.State, typeof(Teleport), "T6-End: Should be Teleport");
        Assert.AreEqual(1, main.Duration, "T6-End: Duration should be reset to Teleport's duration(1)");

        // --- T7: 瞬身 (豁免) ---
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.AreEqual(1, main.Duration, "T7-End: Teleport duration (exempt) should still be 1");

        // --- T8: 瞬身計時1, 結束時轉換為 Normal ---
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.IsInstanceOfType(main.State, typeof(Normal), "T8-End: Should be Normal");
        Assert.AreEqual(1, main.Duration, "T8-End: Duration should be reset to Normal's duration(1)");
    }
    #endregion

    #region 驗證「蓄力」狀態在遭受攻擊時，會如預期般被打斷並恢復成「正常」狀態。
    [TestMethod]
    public void StockpileState_IsInterruptedByAttack_ReturnsToNormal()
    {
        // Arrange
        var (map, main) = TestHelper.CreateTestEnvironment();
        var monster = new Monster(Direction.Up);
        main.EnterState(new Stockpile(main));

        // Act
        // 在蓄力期間受到攻擊
        main.UnderAttack(monster.Ap, map);

        // Assert
        Assert.IsInstanceOfType(main.State, typeof(Normal), "Stockpile should be interrupted and return to Normal state after being attacked.");
        Assert.AreEqual(250, main.Hp, "Should take damage when interrupted.");
    }
    #endregion

    # region 這個測試驗證一個已存在的狀態（中毒）是否能被新獲得的狀態（無敵）正確地覆蓋掉其效果。
    [TestMethod]
    public void PoisonedState_IsOverwrittenBySuperStar_BecomesInvincible()
    {
        // --- Arrange (準備) ---
        var (map, main) = TestHelper.CreateTestEnvironment();
        var initialHp = main.Hp;

        // --- Act & Assert: Part 1 (驗證中毒效果) ---
        // 進入中毒狀態
        main.EnterState(new Poisoned(main));

        // 模擬 T0 (豁免回合)
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.AreEqual(initialHp - 15, main.Hp, "T0: Entering Poisoned state should lose 15 HP at start of turn");

        // 模擬 T1 (第一個計時回合)
        var hpAfterT0 = main.Hp;
        TestHelper.SimulateRound(map, main, (m, p) => { });
        Assert.AreEqual(hpAfterT0 - 15, main.Hp, "T1: Should lose another 15 HP from poison");
        Assert.IsInstanceOfType(main.State, typeof(Poisoned), "Should still be poisoned");

        // --- Act & Assert: Part 2 (驗證狀態覆寫) ---
        // 在中毒期間，獲得無敵狀態
        main.EnterState(new Invincible(main));
        Assert.IsInstanceOfType(main.State, typeof(Invincible), "State should now be Invincible");
        Assert.AreEqual(2, main.Duration, "Duration should be reset to Invincible's duration");

        // 模擬 T2 (進入無敵後的豁免回合)
        var hpBeforeInvincibleTurn = main.Hp;
        TestHelper.SimulateRound(map, main, (m, p) => { });

        // 斷言：HP 不再因中毒而減少
        Assert.AreEqual(hpBeforeInvincibleTurn, main.Hp, "HP should NOT decrease from poison after becoming Invincible");
        Assert.IsInstanceOfType(main.State, typeof(Invincible), "Should remain Invincible");
    }
    #endregion

    #region 這兩個測試驗證攻擊是否能正確地與怪物和障礙物互動。
    [TestMethod]
    public void EruptingState_Attack_ClearsAllMonstersAndNotObstacles()
    {
        // --- Arrange ---
        var (map, main) = TestHelper.CreateTestEnvironment(5, 5);
        // 在地圖上放置多個怪物和障礙物
        var monster1 = new Monster(Direction.Up);
        var monster2 = new Monster(Direction.Up);
        var obstacle = new Obstacle(Direction.Up);
        map.PlaceObject(monster1, 0, 0);
        map.PlaceObject(obstacle, 2, 0);
        map.PlaceObject(monster2, 4, 4);

        // 讓主角進入爆發狀態
        main.EnterState(new Erupting(main));

        // --- Act ---
        // 主角執行攻擊 (在我們的測試中，直接呼叫 Attack)
        main.Attack(map, main.ActionDetails);

        // --- Assert ---
        Assert.IsNull(map.GetObjectAt(0, 0), "Erupting attack should clear monster at (0,0)");
        Assert.IsNull(map.GetObjectAt(4, 4), "Erupting attack should clear monster at (4,4)");
        Assert.IsInstanceOfType(map.GetObjectAt(2, 0), typeof(Obstacle), "Erupting attack should NOT clear the obstacle");
    }

    [TestMethod]
    public void NormalState_LineAttack_IsBlockedByObstacle()
    {
        // --- Arrange ---
        var (map, main) = TestHelper.CreateTestEnvironment(6, 5);
        main.FaceInDirection = Direction.Right; // 確保主角面向右邊

        // 佈置場景: 主角 -> 怪物A -> 障礙物 -> 怪物B
        var monsterA = new Monster(Direction.Up);
        var obstacle = new Obstacle(Direction.Up);
        var monsterB = new Monster(Direction.Up);
        map.PlaceObject(monsterA, 3, 2); // (2,2)是主角位置
        map.PlaceObject(obstacle, 4, 2);
        map.PlaceObject(monsterB, 5, 2);

        // --- Act ---
        main.Attack(map, main.ActionDetails);

        // --- Assert ---
        Assert.IsNull(map.GetObjectAt(3, 2), "Monster A (before obstacle) should be cleared.");
        Assert.IsNotNull(map.GetObjectAt(4, 2), "Obstacle should remain.");
        Assert.IsInstanceOfType(map.GetObjectAt(4, 2), typeof(Obstacle));
        Assert.IsNotNull(map.GetObjectAt(5, 2), "Monster B (after obstacle) should NOT be cleared.");
        Assert.IsInstanceOfType(map.GetObjectAt(5, 2), typeof(Monster));
    }
    #endregion

    #region 這個測試驗證在極端情況下（地圖全滿）程式是否能穩健地處理。
    [TestMethod]
    public void TeleportState_OnMapFull_StaysInPlaceAndNotCrash()
    {
        // --- Arrange ---
        // 建立一個 2x1 的小地圖，只夠放主角和一個障礙物
        var (map, main) = TestHelper.CreateTestEnvironment(2, 1);
        // 將主角放在 (0,0)
        map.RemoveObjectAt(2, 2); // 移除預設的主角
        map.PlaceObject(main, 0, 0);

        // 用障礙物填滿剩下的格子
        var obstacle = new Obstacle(Direction.Up);
        map.PlaceObject(obstacle, 1, 0);

        var initialPosition = (main.X.Value, main.Y.Value);

        // --- Act ---
        // 進入瞬身狀態並模擬一整個生命週期
        main.EnterState(new Teleport(main));
        TestHelper.SimulateRound(map, main, (m, p) => { }); // 豁免回合
        TestHelper.SimulateRound(map, main, (m, p) => { }); // 計時回合，結束時瞬移+變身

        var finalPosition = (main.X.Value, main.Y.Value);

        // --- Assert ---
        Assert.AreEqual(initialPosition, finalPosition, "Character should stay in place when the map is full.");
        Assert.IsInstanceOfType(main.State, typeof(Normal), "Character should still become Normal even if teleport fails.");
        // 這個測試最重要的斷言是它沒有拋出異常並順利完成
    }
    #endregion
}
