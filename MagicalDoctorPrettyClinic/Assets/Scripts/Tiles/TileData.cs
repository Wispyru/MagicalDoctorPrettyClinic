using UnityEngine;

public class TileData
{
    public TileType type;
    public Vector2Int gridPosition;

    public TileData(TileType type, Vector2Int pos)
    {
        this.type = type;
        this.gridPosition = pos;
    }
}