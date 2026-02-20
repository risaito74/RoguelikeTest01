# Task List: Minimal Enemy Implementation

- [x] `Enemy` クラスの作成
    - [x] `Enemy.cs` を `Assets/Scripts` に作成
    - [x] 名前、シンボル、座標、生存フラグ、メッセージ等のプロパティ定義
- [x] `GameManager` へのエネミー管理機能の追加
    - [x] `Enemy` リストの定義
    - [x] `InitEnemies` メソッドの実装（スライム(1,3)の配置、壁チェック）
    - [x] `PlaceCharacters` メソッドの更新（エネミーの描画）
- [x] プレイヤー移動処理 (`OnMoveButtonClicked`) の更新
    - [x] エネミーへの衝突判定（攻撃・撃破処理）の実装
    - [x] 移動後の隣接判定（"Slime ga iru!" メッセージ）の実装
- [x] 動作確認
    - [x] (1,3)にスライムが表示されるか
    - [x] スライムに体当たりして倒せるか
    - [x] スライムの隣にとまったらメッセージが出るか
