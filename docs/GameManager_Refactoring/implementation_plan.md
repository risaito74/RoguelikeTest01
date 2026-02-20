# GameManager リファクタリング計画

## 目的
`GameManager.cs` をリファクタリングし、静的なマップデータ (`baseMapData`) と動的なグリッドデータ (`gridData`) を分離することで、マップ描画の効率化と拡張性の向上を図ります。
従来の「全マスループによるプレイヤー位置判定」を廃止し、「レイヤー書き込み方式」に変更することで、将来的なNPCやエネミーの追加を容易にします。

## ユーザー確認事項
> [!NOTE]
> **NPC・エネミーの実装について**
> 要件定義書には `npcList` や `enemyList` の記述がありますが、現行のコードには関連クラスが存在しません。
> 今回のリファクタリングでは **「プレイヤーの描画」のみ** を実装し、NPC・エネミーの配置箇所にはコメントを残す形で対応します。

## 変更内容

### Assets/Scripts
#### [MODIFY] [GameManager.cs](file:///c:/unity/RoguelikeTest01/Assets/GameManager.cs)
- **データ構造**:
    - `string[] mapData` を削除。
    - `static readonly string[] baseMapData` を追加（変更不可のマップ設計図）。
    - `char[][] gridData` を追加（動的な盤面データ）。
- **初期化処理**:
    - `InitGrid()` を追加し、`baseMapData` のサイズに合わせて `gridData` を初期化。
    - `Start()` 内で `InitGrid()` を呼び出す。
- **描画パイプライン**:
    - `ResetGrid()` の実装: `baseMapData` の内容を `gridData` にコピー。
    - `PlaceCharacters()` の実装:
        - 現状は `playerPosition` にプレイヤー（`@`）を配置する処理のみ実装。
        - *NPC・エネミー配置用ロジックのプレースホルダー（コメント）を追加。*
    - `RenderGrid()`（または `UpdateMapDisplay` 改修）:
        - `gridData` から表示用文字列を生成し `mapText` に反映。
    - `UpdateMapDisplay()` を更新し、`ResetGrid()` → `PlaceCharacters()` → `RenderGrid()` の順で呼び出すように変更。
- **移動ロジック**:
    - `OnMoveButtonClicked` 内の壁判定を `baseMapData`（または `gridData`）を参照するように更新。

## 検証計画

### 手動検証
- **プレイテスト**: Unity エディタで実行。
    - 開始時にマップが正しく描画されるか確認。
    - ボタン操作でプレイヤー（`@`）が正しく移動するか確認。
    - 壁（`#`）との衝突判定が正しく機能するか確認。
    - 基本的なゲームプレイに退行がないことを確認。
