# HFSM 事件冒泡 / 消耗、插件化與 Forces→Patterns→Resulting Context 整理

> 目的：彙整我（Copilot）與 Gemini 對「事件冒泡（Bubble）/消耗（Consume）」的討論，並以架構師觀點用 **Forces → Problem → Patterns → Resulting Context** 的方式整理成可放進專案設計文件的版本。citeturn1search3turn2search4

---

## 1. 背景：題目為何逼出 Bubble / Consume？

- 題目明確要求：當成員送出「標記機器人」且符合指令條件的訊息時，機器人仍要**先依據當前狀態處理該訊息**，**之後再執行指令**（例如狀態切換）。citeturn1search1turn1search3
- 具體例子：管理員送 `king @bot`，機器人要先回覆當前狀態輪播訊息（如 `good to hear @<發送者>`），然後才進入 `KnowledgeKing` 並出題。citeturn1search1turn1search3

這個「**同一個 event 需要先在 leaf 做行為、再讓父層有機會解析指令並轉移**」的順序需求，是事件冒泡機制最直接的觸發點。citeturn1search3turn2search4

---

## 2. 概念釐清：Bubble vs. Consume（用一句話就記住）

### 2.1 Bubble（冒泡）

> **子層（leaf / 子狀態機）已處理了某些事情（可能執行 action），但事件不結束，仍要往父層傳遞，讓父層也能再處理一次。**citeturn1search3turn2search4

- 直觀比喻（Gemini）：像「第一線處理完先回覆，但仍向上呈報讓主管決策是否啟動大方案」。citeturn2search4

### 2.2 Consume（消耗）

> **事件在此層被視為處理完畢，不再往父層傳遞。**citeturn1search3turn2search4

- 直觀比喻（Gemini）：像「第一線已完成關鍵決策（或處理已足夠），主管不需要再插手」。citeturn2search4

> ⚠️ 重要：**Bubble/Consume 是「事件傳遞（propagation）」決策，不等同於 internal/external transition 的分類**；internal/external 常被用來推導預設 heuristics，但兩者不是同一軸。citeturn1search3

---

## 3. 預設規則（Default Heuristic）與例外（Override）

### 3.1 預設直覺：internal bubble / external consume

- **To == null（internal transition）**：通常只是副作用（回覆訊息/記錄/累加），不想阻止父層再處理 → **預設 Bubble**。citeturn1search3
- **To != null（external transition）**：真的切換狀態，通常代表事件已被處理完 → **預設 Consume**。citeturn1search3

### 3.2 為何必須允許例外覆寫？

- 題目中的「知識王答題」：答題訊息也是 `[new message]`（甚至也可能 `@bot`），但它應該只由 `KnowledgeKing.Questioning` 處理，不該冒泡到父層觸發通用規則或指令解析。citeturn1search3turn2search4
- 因此即便是 **internal transition（To==null）**，也可能必須 **Force Consume**（「Handled 但不冒泡」）。citeturn1search3turn2search4

> 結論：`internal bubble / external consume` 只是預設 heuristic；**必須具備 Override 能力**，否則 edge case（答題）會打破硬規則。citeturn1search3turn2search4

---

## 4. 用題目情境落地（兩個典型案例）

### 4.1 `king @bot`（需要 Bubble）

- leaf（例如 `Normal.DefaultConversation`）先做回覆輪播（internal action）。citeturn1search1turn1search3
- leaf 不消耗事件 → **Bubble** 給父層。citeturn1search3
- 父層解析到 `king` 指令 → external transition 到 `KnowledgeKing`（通常 consume）。citeturn1search1turn1search3

### 4.2 KnowledgeKing 答題（需要 Consume）

- `KnowledgeKing.Questioning` 接到答案訊息（同樣是 `[new message]`）。citeturn2search4turn1search1
- 子層執行判斷/計分/出下一題等處理，事件應在此結束 → **Consume**，避免父層誤判成通用指令。citeturn1search3turn2search4

---

## 5. 需要回傳什麼 Result？（Decorator 與 Strategy 的溝通橋樑）

Gemini 建議 `FsmResult` 至少包含：citeturn2search4

- `Handled`：有沒有人處理這個事件？citeturn2search4
- `StateChanged`：有沒有發生狀態轉移？citeturn2search4

在 HFSM 的實作中，這個結果是用來讓 Decorator/Policy 決定「是否繼續向上傳遞」。citeturn1search3turn2search4

> 實務補強：如果只用 `Handled/StateChanged`，在一些情境可能不夠精準，因此也常見加上 `Propagation`（Bubble/Consume）或 `ConsumptionHint` 來表達 override。這能把「預設 vs 覆寫」清楚化。citeturn1search3

---

## 6. 架構師觀點：Forces → Problem → Patterns → Resulting Context

### 6.1 Forces（受力／約束）

1) **題目行為約束：同一事件需先子層行為、後父層指令轉移**（需 child-first + 仍能給 parent 第二次處理機會）。citeturn1search1turn1search3

2) **核心 FSM 需保持乾淨（SRP）**：核心只專注 transition 查找與 entry/exit/action 節奏，不應因 HFSM 而膨脹成 if/else（例如 `if(CurrentState is SubMachineState)`）。citeturn1search3

3) **OCP + 插件化要求**：題目要求子狀態機支援最好做成插件，引入才有；支援/取消支援時不得修改既有 `FiniteStateMachine` 程式碼。citeturn1search1turn1search3turn2search4

4) **消耗規則具變動性**：預設 heuristic 會被答題等 edge case 打破，必須可替換/可覆寫。citeturn1search3turn2search4

### 6.2 Problem（問題定義）

> **如何在不修改核心 FSM 的前提下，賦予其「階層式事件傳遞（先子後父）」能力，同時讓「事件是否繼續向上傳遞」的規則能依情境彈性改變（可覆寫）？**citeturn1search3turn2search4

### 6.3 Patterns（採用的模式）

#### (1) Decorator（裝飾者）：解決「路由流程擴充」

- 用 wrapper 包住 core FSM，在 wrapper 中實作「先進子機器跑，再決定是否交給父機器」的事件路由。citeturn1search3turn2search4
- 使 core `FiniteStateMachine` **Close for modification**，HFSM 行為透過外層 **Open for extension** 加上去。citeturn1search3turn2search4

#### (2) Strategy（策略）：解決「消耗判斷可變」

- 把 bubble/consume 的決策抽成 `IConsumptionPolicy`（或等價介面）。citeturn1search3turn2search4
- Decorator 只負責「跑流程」與「詢問策略」，策略回覆 Bubble 或 Consume，避免硬規則被答題等例外打破。citeturn1search3turn2search4

#### (3) Plugin（插件）：解決「可拔插 HFSM 能力」

- 子狀態機（Sub-state machine）支援以 plugin 模組提供；Client 引入 plugin 才能使用 HFSM 功能，未引入則保持平面 FSM。citeturn1search1turn1search3turn2search4
- 讓「支援/取消支援子狀態機」不需修改核心 FSM，符合題目 OCP 要求。citeturn1search1turn2search4

### 6.4 Resulting Context（導入模式後得到的情境／結果）

導入 **Decorator + Strategy + Plugin** 後，架構會呈現：citeturn1search3turn2search4

- **Core FSM 保持簡潔**：仍只負責 transition 查找與 entry/exit/action 節奏（SRP）。citeturn1search3
- **HFSM 能力可插拔**：有 plugin 就有 SubMachineState 與 child-first 冒泡路由；沒有 plugin 就是平面 FSM。citeturn1search1turn2search4
- **事件傳遞規則可替換**：可以用 DefaultPolicy（internal bubble/external consume）+ Override，或 StrictPolicy（Handled=true 即 consume）等，因情境調整。citeturn1search3turn2search4
- **題目案例可被精準驗收**：
  - `king @bot`：leaf 先回覆，再 bubble 給父層做轉移。citeturn1search1turn1search3
  - 答題：child Handled 但 consume，父層不會收到事件。citeturn2search4turn1search3

---

## 7. Gemini 小測試（驗證理解）

題目：答題狀態輸入答案；子狀態機處理但未切狀態（internal）；採 StrictPolicy：只要 `Handled=true` 就不冒泡。citeturn2search4

1) `Handled` 是 true/false？→ **true**（子狀態確實處理了事件）。citeturn2search4
2) Strategy 回覆 Bubble/Consume？→ **Consume**（StrictPolicy：Handled 即 consume）。citeturn2search4
3) 父狀態機會收到事件嗎？→ **不會**（被 consume，停止冒泡）。citeturn2search4

---

## 8. 可直接貼到設計文件的一句話版本

> 題目要求同一事件需「先由子狀態處理，再由父層解析指令並可能轉移」，因此需要事件冒泡（Bubble）；但冒泡會造成某些事件（如答題）不該再往上傳遞，故需可覆寫的消耗策略（Consume Policy）。為滿足「不修改核心 FSM、子狀態機能力可插件化」的 OCP 要求，採用 Decorator 實作 child-first 路由、Strategy 抽離消耗判斷，並以 Plugin 提供 HFSM 擴充能力。citeturn1search1turn1search3turn2search4
