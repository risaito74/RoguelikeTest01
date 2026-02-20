using UnityEngine;
using UnityEngine.UI; // Button操作用
using System.Collections.Generic;
using TMPro;        // TextMeshProを使用するための名前空間

public class GameManager : MonoBehaviour
{
    // private string[] mapData; // REMOVED: Old map data
    
    // 静的なマップデータ（変更不可の設計図）
    private static readonly string[] baseMapData = {
        "#####  ###   ",
        "#...####.####",
        "#...........#",
        "#...#########",
        "#####        "
    };

    // 動的なグリッドデータ（実際にキャラが乗る盤面）
    private char[][] gridData;

    // NPCリストを追加
    private List<NPC> npcList;

    // エネミーリストを追加
    private List<Enemy> enemyList;

    private Vector2Int playerPosition;

    // インスペクタでアタッチしたTMPを参照
    public TMP_Text mapText;
    public TMP_Text messageText;
    public TMP_Text hpText; // HP表示用

    // ボタンUIの参照
    public Button upButton;
    public Button downButton;
    public Button leftButton;
    public Button rightButton;

    // プレイヤーのパラメータ
    private int playerHP;
    private const int playerMaxHP = 10;
    private bool isPlayerDead = false; // 死亡フラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPlayerDead = false; // フラグ初期化
        playerPosition = new Vector2Int(1, 1); // プレイヤーの初期位置を設定
        playerHP = playerMaxHP; // HP初期化
        InitGrid();             // グリッドの初期化
        InitNPCs();             // NPCの初期化
        InitEnemies();          // エネミーの初期化
        UpdateMapDisplay();     // 最初のマップ表示を更新
        UpdateHPDisplay();      // HP表示更新

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

    // NPCの初期化
    void InitNPCs()
    {
        npcList = new List<NPC>();

        // エミ (E, (3,3)) の追加
        Vector2Int emiPos = new Vector2Int(3, 3);
        
        // 壁チェック（安全のためbaseMapDataで確認）
        if (baseMapData[emiPos.y][emiPos.x] == '#')
        {
            Debug.LogError($"NPC 'Emi' cannot be placed at {emiPos} because it is a wall!");
        }

        npcList.Add(new NPC("Emi", 'E', emiPos, "Watashi ha Emi dayo!"));
    }

    // エネミーの初期化
    void InitEnemies()
    {
        enemyList = new List<Enemy>();

        // スライム (S, (1,3)) の追加
        Vector2Int slimePos1 = new Vector2Int(1, 3);
        if (baseMapData[slimePos1.y][slimePos1.x] == '#')
        {
            Debug.LogError($"Enemy 'Slime' cannot be placed at {slimePos1} because it is a wall!");
        }
        enemyList.Add(new Enemy("Slime", 'S', slimePos1));

        // 2体目のスライム (S, (8,1)) の追加
        Vector2Int slimePos2 = new Vector2Int(8, 1);
        if (baseMapData[slimePos2.y][slimePos2.x] == '#')
        {
            Debug.LogError($"Enemy 'Slime' cannot be placed at {slimePos2} because it is a wall!");
        }
        enemyList.Add(new Enemy("Slime", 'S', slimePos2));
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
        // 1. NPCの配置
        foreach (var npc in npcList)
        {
            gridData[npc.pos.y][npc.pos.x] = npc.symbol;
        }

        // 2. エネミーの配置
        foreach (var enemy in enemyList)
        {
            if (!enemy.IsDead)
            {
                gridData[enemy.Pos.y][enemy.Pos.x] = enemy.Symbol;
            }
        }

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
        if (isPlayerDead) return; // 死亡時は操作無効

        Vector2Int nextPos = playerPosition;

        // 方向によって座標を計算
        if (direction == "Up") nextPos.y -= 1;
        if (direction == "Down") nextPos.y += 1;
        if (direction == "Left") nextPos.x -= 1;
        if (direction == "Right") nextPos.x += 1;

        // 1. 壁チェック
        if (baseMapData[nextPos.y][nextPos.x] == '#')
        {
            messageText.text = "Kabe ni butsukatta!"; // 壁に当たったメッセージ
            return;
        }

        // 2. NPCチェック
        foreach (var npc in npcList)
        {
            if (npc.pos == nextPos)
            {
                // エミの場合はHP回復
                if (npc.name == "Emi")
                {
                    if (playerHP < playerMaxHP)
                    {
                        playerHP = playerMaxHP;
                        UpdateHPDisplay();
                        messageText.text = "HP wo kaihuku shitayo!";
                    }
                    else
                    {
                        messageText.text = "Watashi ha Emi dayo!";
                    }
                }
                else
                {
                    messageText.text = npc.message; // その他のNPC
                }
                return; // 移動せずに終了（衝突）
            }
        }

        // 3. エネミーチェック（攻撃・撃破）
        foreach (var enemy in enemyList)
        {
            if (!enemy.IsDead && enemy.Pos == nextPos)
            {
                // エネミーを倒す
                enemy.IsDead = true;
                playerHP = Mathf.Max(0, playerHP - 1); // HP減少（0未満にならないように）
                UpdateHPDisplay();
                messageText.text = $"{enemy.Name} wo taoshita!";
                
                // プレイヤーは移動する（踏み込む）
                playerPosition = nextPos;
                UpdateMapDisplay();
                CheckEnemyAdjacency(); // 移動後の隣接チェック
                CheckEnemyRespawn();   // リスポーンチェック
                
                // 死亡チェック
                if (playerHP <= 0)
                {
                    PlayerDeath();
                }
                return; 
            }
        }

        // 4. 移動実行
        playerPosition = nextPos;
        UpdateMapDisplay(); // 画面を更新！
        messageText.text = "Anata ha aruita.";
        
        CheckEnemyAdjacency(); // 移動後の隣接チェック
        CheckEnemyRespawn();   // リスポーンチェック
    }

    // プレイヤー死亡処理
    void PlayerDeath()
    {
        isPlayerDead = true;
        
        // GameOverメッセージ表示（赤色）
        messageText.text = "<color=red>GameOver...</color>";

        // ボタン操作を無効化（グレーアウト）
        if (upButton != null) upButton.interactable = false;
        if (downButton != null) downButton.interactable = false;
        if (leftButton != null) leftButton.interactable = false;
        if (rightButton != null) rightButton.interactable = false;
    }

    // エネミーのリスポーンチェック
    void CheckEnemyRespawn()
    {
        foreach (var enemy in enemyList)
        {
            if (enemy.IsDead)
            {
                float distance = Vector2Int.Distance(playerPosition, enemy.Pos);
                if (distance >= 5.0f)
                {
                    enemy.IsDead = false; // 復活
                }
            }
        }
    }

    // HP表示の更新
    void UpdateHPDisplay()
    {
        if (hpText != null)
        {
            hpText.text = $"HP: {playerHP} / {playerMaxHP}";
        }
    }
    // 周囲にエネミーがいるかチェックしてメッセージを表示
    void CheckEnemyAdjacency()
    {
        // 上下左右のオフセット
        Vector2Int[] directions = {
            new Vector2Int(0, 1),  // Up
            new Vector2Int(0, -1), // Down
            new Vector2Int(-1, 0), // Left
            new Vector2Int(1, 0)   // Right
        };

        foreach (var enemy in enemyList)
        {
            if (enemy.IsDead) continue;

            foreach (var dir in directions)
            {
                if (playerPosition + dir == enemy.Pos)
                {
                    messageText.text = $"{enemy.Name} ga iru!";
                    return; // 1体見つけたら優先して表示終了
                }
            }
        }
    }
}
