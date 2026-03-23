using UnityEngine;
using System.Collections.Generic;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private List<TileData> _tileDataOptions;
    [SerializeField] private float _tileSpacing = 1f;

    private Tile[,] _grid;
    private int _width;
    private int _height;

    /// <summary>
    /// Generates the full grid at level start and passes it to the GridManager.
    /// </summary>
    public void GenerateGrid(int width, int height)
    {
        _width = width;
        _height = height;
        _grid = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _grid[x, y] = SpawnTile(x, y);
            }
        }

        _gridManager.SetGrid(_grid, width, height);
    }

    /// <summary>
    /// Spawns a single tile at the given grid position.
    /// </summary>
    private Tile SpawnTile(int x, int y)
    {
        Vector2 spawnPosition = GetWorldPosition(x, y);
        GameObject tileObject = Instantiate(_tilePrefab, spawnPosition, Quaternion.identity);
        Tile tile = tileObject.GetComponent<Tile>();

        TileData tileData = GetNonMatchingTileData(x, y);
        tile.Initialize(x, y, tileData);

        return tile;
    }

    /// <summary>
    /// Returns a TileData that won't cause a 3-in-a-row at the given position, but allows pairs.
    /// </summary>
    private TileData GetNonMatchingTileData(int x, int y)
    {
        List<TileData> validOptions = new List<TileData>(_tileDataOptions);

        if (x >= 2)
        {
            TileType leftOne = _grid[x - 1, y].Data.Type;
            TileType leftTwo = _grid[x - 2, y].Data.Type;

            if (leftOne == leftTwo)
                validOptions.RemoveAll(data => data.Type == leftOne);
        }

        if (y >= 2)
        {
            TileType belowOne = _grid[x, y - 1].Data.Type;
            TileType belowTwo = _grid[x, y - 2].Data.Type;

            if (belowOne == belowTwo)
                validOptions.RemoveAll(data => data.Type == belowOne);
        }

        return validOptions[Random.Range(0, validOptions.Count)];
    }

    /// <summary>
    /// Converts a grid coordinate to a world position.
    /// </summary>
    private Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(x * _tileSpacing, y * _tileSpacing);
    }
}