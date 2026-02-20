# リスタート機能 実装タスクリスト

- [x] 要件確認 <!-- id: 0 -->
    - [x] RestartButtonの表示制御（初期非表示 -> 死亡時表示）
    - [x] RestartButton押下時のシーン再ロード
- [x] 実装計画の作成 <!-- id: 1 -->
- [x] リスタート機能の実装 <!-- id: 2 -->
    - [x] `GameManager` に `restartButton` フィールドを追加
    - [x] `Start` でボタンを非表示＆リスナー登録
    - [x] `PlayerDeath` でボタンを表示
    - [x] シーン再ロード処理の実装 (`SceneManager` 利用)
- [x] 検証 <!-- id: 3 -->
- [x] 追加改修: 死亡時にHP表示を赤色にする <!-- id: 4 -->
