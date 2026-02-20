# Issue #4 実装計画

## 目標
Issue #4 で記述されているプレイヤーのHPシステム、簡易戦闘ダメージ、NPCによる回復、およびエネミーのリスポーン機能を実装します。

## ユーザーレビューが必要な事項
> [!IMPORTANT]
> **HPテキストUI:** `GameManager` に新しい `TMP_Text` フィールド `hpText` を追加します。シーン上に TextMeshPro オブジェクトを作成し、インスペクターでこのフィールドに割り当てる必要があります。

> [!NOTE]
> **エネミーの配置:** 
> 添付画像（プレイヤーと同じ行の赤丸位置）に基づき、2体目のスライムの座標を **`(10, 1)`** に設定します。
> （コード上の座標 `y` は0から始まるため、2行目は `1` になります）

## 提案される変更

### Assets/Scripts
#### [MODIFY] [GameManager.cs](file:///c:/unity/RoguelikeTest01/Assets/Scripts/GameManager.cs)
- `public TMP_Text hpText` フィールドを追加。
- `private int playerHP` と `private const int playerMaxHP = 10` を追加。
- `Start` メソッドでHPを初期化し、UIを更新。
- `InitEnemies` を更新し、2体目のスライムを `(10, 1)` に追加。
- `OnMoveButtonClicked` 内の処理:
    - **ダメージ**: エネミーを倒した際、`playerHP` を 1 減らす。
    - **回復**: NPC「エミ」にぶつかった際、以下の条件で分岐。
        - **HP < MaxHP**: `playerHP` を全回復し、メッセージ "HP wo kaihuku shitayo!" を表示。
        - **HP == MaxHP**: 回復せず、メッセージ "Watashi ha Emi dayo!" を表示。
    - **リスポーン**: 移動後に新しいメソッド `CheckEnemyRespawn()` を呼び出す。
- `CheckEnemyRespawn()` を追加:
    - 死んでいるエネミーをループ処理。
    - プレイヤーとの距離が 5.0f 以上離れていれば、エネミーを復活 (`IsDead = false`) させる。
- UIテキストを更新する `UpdateHPDisplay()` を追加。

## 検証計画

### 手動検証
1.  **HP表示**: ゲームを実行し、画面に "HP: 10/10" と表示されることを確認。
2.  **ダメージ**: `(1, 3)` または `(10, 1)` へ移動し、スライムを攻撃する。
    -   スライムが消えることを確認。
    -   メッセージ "Slime wo taoshita!" を確認。
    -   HPが 9/10 になることを確認。
3.  **リスポーン**: 倒したスライムの位置から 5マス以上離れる。
    -   スライムがマップ上に再出現することを確認。
4.  **回復と会話**:
    - **HP減時**: ダメージを受けた状態で NPC エミ `(3, 3)` に体当たりする。
        - メッセージ "HP wo kaihuku shitayo!" を確認。
        - HPが 10/10 に回復することを確認。
    - **HP満タン時**: HP満タンの状態で NPC エミに体当たりする。
        - メッセージ "Watashi ha Emi dayo!" を確認。
