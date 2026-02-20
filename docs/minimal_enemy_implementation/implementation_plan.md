# 実装計画: エネミーの簡易実装 (Minimal Enemy Implementation)

## 目標
GitHub Issue #1 に基づき、エネミー（スライム）の最小限の実装を行う。

## ユーザーレビューが必要な事項
- 特になし（Issueの要件通り）

## 変更内容

### 新規作成
#### [NEW] [Enemy.cs](file:///c:/unity/RoguelikeTest01/Assets/Scripts/Enemy.cs)
- `Enemy` クラス（または構造体）を定義
- プロパティ: `Name` (string), `Symbol` (char), `Pos` (Vector2Int), `IsDead` (bool), `Message` (string, 任意)

### 変更
#### [MODIFY] [GameManager.cs](file:///c:/unity/RoguelikeTest01/Assets/Scripts/GameManager.cs)
- `enemyList` フィールドを追加
- `InitEnemies()` メソッド追加
    - スライム('S')を(1, 3)に生成
    - 生成位置が壁('#')ならエラーログ出力
- `PlaceCharacters()` メソッド更新
    - エネミーのシンボルを `gridData` に描画（`IsDead` でない場合）
- `OnMoveButtonClicked()` メソッド更新
    - 移動先座標判定にエネミー判定を追加
        - 移動先に生存エネミーがいる場合：攻撃処理（エネミー死亡、"Slime wo taoshita!" メッセージ、プレイヤー移動）
    - 移動完了後の処理に追加
        - 隣接（上下左右）に生存エネミーがいる場合："Slime ga iru!" メッセージを表示（優先度高）

## 検証計画

### 手動検証
1. **Unityエディタで再生**
2. **初期配置確認**: (1, 3) に 'S' が表示されていることを確認。
3. **壁判定ログ確認**: コード上でわざと壁座標(0,0など)に配置してみて、Consoleにエラーログが出るか確認（オプション）。
4. **隣接メッセージ確認**: カニ歩き等で(1, 4)や(2, 3)に移動し、"Slime ga iru!" と表示されるか確認。
5. **攻撃・撃破確認**: (1, 3) に向かって移動し、'S' が消え、"Slime wo taoshita!" と表示され、プレイヤーが(1, 3)に移動することを確認。
