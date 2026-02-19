# NPC実装計画 (Simple Version)

## 目的
GitHub Issue #2 の要件に基づき、NPC「エミ」をゲーム内に配置し、プレイヤーとのインタラクション（メッセージ表示）を実装します。

## GitHub Issue #2 要件サマリ
- **名前**: エミ
- **表示**: 'E'
- **配置**: (3, 3) 固定
- **エラー処理**: 配置位置が壁('#')ならログエラーを出力
- **挙動**: 動かない
- **衝突**: プレイヤーが衝突すると「Watashi ha Emi dayo!」と表示。移動はブロックされる（重ならない）。

## 提案される変更

### Assets/Scripts
#### [NEW] [NPC.cs](file:///c:/unity/RoguelikeTest01/Assets/NPC.cs)
- シンプルなデータクラスとして定義。
```csharp
public class NPC
{
    public string name;
    public char symbol;
    public Vector2Int pos;
    public string message;
}
```

#### [MODIFY] [GameManager.cs](file:///c:/unity/RoguelikeTest01/Assets/GameManager.cs)
- **メンバ変数**:
    - `List<NPC> npcList` を追加。
- **初期化 (`Start` / `InitNPCs`)**:
    - `InitNPCs()` メソッドを作成。
    - エミのインスタンスを作成し `npcList` に追加。
    - `baseMapData` を参照し、(3,3)が壁なら `Debug.LogError` を出力。
- **描画 (`PlaceCharacters`)**:
    - `npcList` をループし、`gridData` に `symbol` を上書き配置する処理のコメントアウトを解除・実装。
- **移動判定 (`OnMoveButtonClicked`)**:
    - 壁判定に加え、予定地にNPCがいるかチェックするロジックを追加。
    - NPCがいる場合：
        - `messageText.text` にそのNPCのメッセージを表示。
        - プレイヤーの移動は行わない（`return`）。

## 検証計画

### 手動検証
1. **表示確認**: ゲーム開始時、(3,3) に 'E' が表示されているか。
2. **衝突確認**: プレイヤーを操作して 'E' にぶつかる。
    - メッセージ "Watashi ha Emi dayo!" が表示されるか。
    - プレイヤーが 'E' の上に重ならずに止まるか。
3. **エラーハンドリング確認**:
    - コード上で一時的にエミの座標を壁の位置（例: (0,0)）に変更し、コンソールにエラーが出るか確認する。
