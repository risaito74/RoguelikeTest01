using UnityEngine;

public class NPC
{
    public string name;
    public char symbol;
    public Vector2Int pos;
    public string message;

    public NPC(string name, char symbol, Vector2Int pos, string message)
    {
        this.name = name;
        this.symbol = symbol;
        this.pos = pos;
        this.message = message;
    }
}
