# Issue #5 プレイヤー死亡処理 実装計画

## 目標
Issue #5 に基づき、プレイヤーのHPが0以下になった際にゲームオーバー状態へ移行する処理を実装します。
これにより、HPが0のまま行動できる「ゾンビ状態」を防ぎ、ゲームオーバーを明確にします。

## ユーザーレビューが必要な事項
> [!IMPORTANT]
> **ボタンUIの参照:** `GameManager` が移動ボタン（Up, Down, Left, Right）の `Button` コンポーネントを制御できるようにする必要があります。
> インスペクタで各ボタンを `GameManager` の新しいフィールドにアタッチする必要があります。

## 提案される変更

### Assets/Scripts
#### [MODIFY] [GameManager.cs](file:///c:/unity/RoguelikeTest01/Assets/Scripts/GameManager.cs)
- `using UnityEngine.UI;` を追加（Button操作のため）。
- `public Button upButton, downButton, leftButton, rightButton;` フィールドを追加。
- `private bool isPlayerDead` フラグを追加。
- `Start` メソッドでフラグをリセット。
- `OnMoveButtonClicked` メソッド:
    - 冒頭で `isPlayerDead` が true なら `return` して操作を無効化。
    - ダメージ処理後に `playerHP <= 0` をチェック。
    - 0以下なら `PlayerDeath()` を呼び出す。
- `PlayerDeath()` メソッドを追加:
    - `isPlayerDead = true` に設定。
    - `messageText` に `<color=red>GameOver...</color>` を表示。
    - 各移動ボタンの `interactable` を `false` に設定してグレーアウトさせる。

## 検証計画

### 手動検証
1.  **死亡確認**:
    - ゲームを実行し、スライムに特攻してHPを0にする。
    - HPが0になった瞬間、メッセージエリアに赤文字で `GameOver...` と表示されることを確認。
    - 同時に移動ボタンがグレーアウトし、押せなくなることを確認。
2.  **移動不可確認**:
    - グレーアウトしたボタンを押しても、プレイヤーが移動しないこと、メッセージが変わらないことを確認。
3.  **NPC回復確認（境界値）**:
    - HP1の状態でエミに会い、回復してから再度死亡までダメージを受けるなど、フラグ管理が正常か確認（今回はリスタート機能がないため、再実行で確認）。
