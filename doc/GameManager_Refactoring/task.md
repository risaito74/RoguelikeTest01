# GameManager リファクタリングタスク

- [ ] 計画
    - [x] `doc/GameManager_実装要件.md` に基づいた実装計画の作成 <!-- id: 0 -->
    - [x] ユーザーとの実装計画レビュー <!-- id: 1 -->
- [ ] 実装
    - [x] `baseMapData` を `static readonly string[]` として定義 <!-- id: 2 -->
    - [x] `gridData` (`char[][]`) の定義と初期化ロジック (`InitGrid`) <!-- id: 3 -->
    - [x] `ResetGrid()` の実装（基本マップをグリッドにコピー） <!-- id: 4 -->
    - [x] `PlaceCharacters()` の実装（現在はプレイヤーのみ） <!-- id: 5 -->
    - [x] `RenderGrid()` の実装（`gridData` からテキスト生成） <!-- id: 6 -->
    - [x] `UpdateMapDisplay()` を新しいパイプラインを使用するように更新 <!-- id: 7 -->
    - [x] 移動ロジックを更新して `baseMapData` との衝突判定を行う <!-- id: 8 -->
- [x] 検証
    - [x] プレイヤーの移動が以前と同様に機能することを確認 <!-- id: 9 -->
    - [x] マップ描画が正しいことを確認 <!-- id: 10 -->
