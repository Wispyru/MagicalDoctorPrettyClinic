using UnityEngine;

public class Grid
{
    public int width;
    public int height;
    public float cellSize;
    private TileData[,] gridArray;

    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridArray = new TileData[width, height];
    }

    public void SetTile(int x, int y, TileData tile)
    {
        if (IsValid(x, y))
            gridArray[x, y] = tile;
    }

    public TileData GetTile(int x, int y)
    {
        return IsValid(x, y) ? gridArray[x, y] : null;
    }

    private bool IsValid(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }
}