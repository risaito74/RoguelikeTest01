using UnityEngine;

public class Enemy
{
    public string Name { get; private set; }
    public char Symbol { get; private set; }
    public Vector2Int Pos { get; set; }
    public bool IsDead { get; set; }
    public string Message { get; private set; } // 倒した時などのメッセージ（拡張用）

    public Enemy(string name, char symbol, Vector2Int pos, string message = "")
    {
        Name = name;
        Symbol = symbol;
        Pos = pos;
        IsDead = false;
        Message = message;
    }
}
