# Issue #5 実装検証手順

## 変更内容
- **GameManager.cs**:
    - `isPlayerDead` フラグと死亡処理 `PlayerDeath()` を追加。
    - HP <= 0 でゲームオーバー状態へ移行。
    - ゲームオーバー時：
        - メッセージ `<color=red>GameOver...</color>` を表示。
        - 上下左右の移動ボタンを非活性化（interactable = false）。

## 検証手順

### 1. Unityエディタ設定
> [!IMPORTANT]
> コードの修正により、`GameManager` コンポーネントに新しいフィールド `Up Button`, `Down Button`, `Left Button`, `Right Button` が追加されています。
> Hierarchyにある各移動ボタン（UpButton, DownButton, LeftButton, RightButton）を、Inspectorの対応するフィールドにアサインしてください。

### 2. プレイ検証
Unityエディタで再生ボタンを押し、以下の動作を確認してください。

1.  **死亡確認**
    - スライムに何度も体当たりしてHPを0にする。
    - HPが0になった瞬間、メッセージエリアに赤文字で `GameOver...` と表示されるか。
    - 画面下の移動ボタンがすべてグレーアウト（無効化）されるか。

2.  **操作不能確認**
    - グレーアウトしたボタンをクリックしても、プレイヤー（@）が動かないことを確認。
    - メッセージが更新されないことを確認。
