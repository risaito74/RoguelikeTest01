using UnityEngine;
using TMPro;        // TextMeshProを使用するための名前空間

public class GameManager : MonoBehaviour
{
    private string[] mapData;
    private Vector2Int playerPosition;

    // インスペクタでアタッチしたTMPを参照
    public TMP_Text mapText;
    public TMP_Text messageText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPosition = new Vector2Int(1, 1); // プレイヤーの初期位置を設定
        SetBaseMap();           // マップの基本データを設定
        UpdateMapDisplay();     // 最初のマップ表示を更新

        messageText.text = "Anata no bouken ga hajimatta!!";    // メッセージを表示
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // マップの基本データを設定
    void SetBaseMap()
    {
        mapData = new string[]
        {
            "#####  ###",
            "#...####.#",
            "#........#",
            "#...######",
            "#####     "
        };
    }

    // マップ表示を更新する関数
    void UpdateMapDisplay()
    {
        string fullMapText = "<line-height=80%><mspace=0.7em>"; // タグを忘れずに

        for (int y = 0; y < mapData.Length; y++)
        {
            for (int x = 0; x < mapData[y].Length; x++)
            {
                // 今のループの座標がプレイヤーの位置と同じなら '@' を足す
                if (x == playerPosition.x && y == playerPosition.y)
                {
                    fullMapText += "@";
                }
                else
                {
                    fullMapText += mapData[y][x];
                }
            }
            fullMapText += "\n"; // 行の終わりに改行！
        }

        mapText.text = fullMapText;
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
        if (mapData[nextPos.y][nextPos.x] != '#')
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
