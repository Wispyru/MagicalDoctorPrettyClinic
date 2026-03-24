using UnityEngine;

public class Grid
{
    public int width;
    public int height;
    public float cellSize;
    private int[,] gridArray;

    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridArray = new int[width, height];
    }

    public void SetValue(int x, int y, int value)
    {
        if (IsValid(x, y))
            gridArray[x, y] = value;
    }

    public int GetValue(int x, int y)
    {
        return IsValid(x, y) ? gridArray[x, y] : -1;
    }
    
    public Vector2Int GetGridPosition(Vector2 anchoredPos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(anchoredPos.x / cellSize),
            Mathf.FloorToInt(anchoredPos.y / cellSize)
        );
    }

    private bool IsValid(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }
}
