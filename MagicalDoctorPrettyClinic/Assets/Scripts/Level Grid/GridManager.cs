using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    private Tile[,] _grid;

    /// <summary>
    /// Receives and stores the generated grid from GridGenerator.
    /// </summary>
    public void SetGrid(Tile[,] grid, int width, int height)
    {
        _grid = grid;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Returns the tile at the given grid position.
    /// </summary>
    public Tile GetTile(int x, int y)
    {
        if (!IsWithinBounds(x, y)) return null;
        return _grid[x, y];
    }

    /// <summary>
    /// Checks whether a given position is within the grid bounds.
    /// </summary>
    private bool IsWithinBounds(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }

    /// <summary>
    /// Placeholder for match detection — will be built out in [14] and [15].
    /// </summary>
    public void CheckForMatches()
    {
        // Stub for [14] and [15]
    }

    /// <summary>
    /// Placeholder for reporting a match to the score system — will be built out in [9].
    /// </summary>
    private void ReportMatch(int matchCount)
    {
        // Stub for [9]
    }
}