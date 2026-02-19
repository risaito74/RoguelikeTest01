using UnityEngine;
using TMPro;        // TextMeshProを使用するための名前空間

public class GameManager : MonoBehaviour
{
    // private string[] mapData; // REMOVED: Old map data
    
    // 静的なマップデータ（変更不可の設計図）
    private static readonly string[] baseMapData = {
        "#####  ###",
        "#...####.#",
        "#........#",
        "#...######",
        "#####     "
    };

    // 動的なグリッドデータ（実際にキャラが乗る盤面）
    private char[][] gridData;

    private Vector2Int playerPosition;

    // インスペクタでアタッチしたTMPを参照
    public TMP_Text mapText;
    public TMP_Text messageText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPosition = new Vector2Int(1, 1); // プレイヤーの初期位置を設定
        InitGrid();             // グリッドの初期化
        UpdateMapDisplay();     // 最初のマップ表示を更新

        messageText.text = "Anata no bouken ga hajimatta!!";    // メッセージを表示
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // グリッドの初期化
    void InitGrid()
    {
        gridData = new char[baseMapData.Length][];
        for (int y = 0; y < baseMapData.Length; y++)
        {
            gridData[y] = baseMapData[y].ToCharArray();
        }
    }

    // マップ表示を更新する関数
    void UpdateMapDisplay()
    {
        ResetGrid();         // 盤面をbaseMapDataでリセット
        PlaceCharacters();   // キャラクターを重ね書き

        // グリッドをテキストに変換して表示
        string fullMapText = "<line-height=80%><mspace=0.7em>"; // タグを忘れずに

        for (int y = 0; y < gridData.Length; y++)
        {
            fullMapText += new string(gridData[y]) + "\n";
        }

        mapText.text = fullMapText;
    }

    // 盤面をbaseMapDataでリセット（前のコマのキャラを消す）
    void ResetGrid()
    {
        for (int y = 0; y < baseMapData.Length; y++)
        {
            // string.CopyTo(開始位置, 転送先配列, 転送先開始位置, 文字数)
            // メモリ確保なしで高速にコピーできます
            baseMapData[y].CopyTo(0, gridData[y], 0, baseMapData[y].Length);
        }
    }

    // キャラクターをレイヤー順に配置
    void PlaceCharacters()
    {
        // 1. NPCの配置（将来実装）
        // foreach (var npc in npcList) gridData[npc.y][npc.x] = npc.symbol;

        // 2. エネミーの配置（将来実装）
        // foreach (var enemy in enemyList) gridData[enemy.y][enemy.x] = enemy.symbol;

        // 3. プレイヤーの配置（最前面）
        gridData[playerPosition.y][playerPosition.x] = '@';
    }

    // Upボタンが押された
    public void OnUpButton()
    {
        OnMoveButtonClicked("Up");
    }

    // Downボタンが押された
    public void OnDownButton()
    {
        OnMoveButtonClicked("Down");
    }

    // Leftボタンが押された
    public void OnLeftButton()
    {
        OnMoveButtonClicked("Left");
    }

    // Rightボタンが押された
    public void OnRightButton()
    {
        OnMoveButtonClicked("Right");
    }

    // 押されたボタンによって座標を更新して壁チェックして画面を更新
    public void OnMoveButtonClicked(string direction)
    {
        Vector2Int nextPos = playerPosition;

        // 方向によって座標を計算
        if (direction == "Up") nextPos.y -= 1;
        if (direction == "Down") nextPos.y += 1;
        if (direction == "Left") nextPos.x -= 1;
        if (direction == "Right") nextPos.x += 1;

        // 💡 ここで「壁（#）じゃないか」のチェックを入れると完璧！
        // baseMapData（動かない地形）で判定するのが安全
        if (baseMapData[nextPos.y][nextPos.x] != '#')
        {
            playerPosition = nextPos;
            UpdateMapDisplay(); // 画面を更新！

            messageText.text = "Anata ha aruita."; // 壁に当たったメッセージ
        }
        else
        {
            messageText.text = "Kabe ni butsukatta!"; // 壁に当たったメッセージ
        }
    }
}
