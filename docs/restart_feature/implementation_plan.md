# リスタート機能 実装計画

## 目標
プレイヤー死亡時にリスタートするためのボタン機能を追加します。
ボタンは通常時は非表示で、ゲームオーバー時に表示され、押すとシーンを再読み込みします。

## ユーザーレビューが必要な事項
> [!IMPORTANT]
> **RestartButtonの作成と割り当て:** 
> シーン上に `Button` (Legacy or TMP) を作成し、`GameManager` の `Restart Button` フィールドにアタッチしてください。
> ボタンの表示/非表示はスクリプト側で制御するため、シーン上では表示状態（Active）のままで構いません（実行時に隠されます）。

## 提案される変更

### Assets/Scripts
#### [MODIFY] [GameManager.cs](file:///c:/unity/RoguelikeTest01/Assets/Scripts/GameManager.cs)
- `using UnityEngine.SceneManagement;` を追加。
- `public Button restartButton;` を追加。
- `Start` メソッド:
    - `restartButton.gameObject.SetActive(false);` で非表示化。
    - `restartButton.onClick.AddListener(OnRestartPostion);` でクリックイベント登録（コード側でやる方が安全）。
- `PlayerDeath` メソッド:
    - `restartButton.gameObject.SetActive(true);` で表示。
- `OnRestartPostion` メソッド (新規):
    - `SceneManager.LoadScene(SceneManager.GetActiveScene().name);` で現在のシーンをリロード。

## 検証計画

### 手動検証
1.  **初期状態確認**:
    - ゲーム開始直後、リスタートボタンが表示されていないことを確認。
2.  **表示確認**:
    - スライムに特攻して死亡する。
    - ゲームオーバーのメッセージと共にリスタートボタンが表示されることを確認。
3.  **動作確認**:
    - リスタートボタンを押す。
    - シーンが再読み込みされ、HPが10に戻り、プレイヤー位置が初期化されることを確認。
