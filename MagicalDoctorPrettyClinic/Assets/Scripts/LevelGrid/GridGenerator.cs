using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float cellSize = 50f;

    public Canvas canvas;
    public RectTransform parentContainer;
    public GameObject cellPrefab;

    public bool centerGrid = true;
    public Vector2 manualOffset;

    private Grid _grid;
    private Tile[,] _tiles;
    
    void Start()
    {
        _grid = new Grid(width, height, cellSize);
        _tiles = new Tile[width, height];
        GenerateUIGrid();
    }
    
    private void GenerateUIGrid()
    {
        Vector2 gridSize = new Vector2(width * cellSize, height * cellSize);
        Vector2 centerOffset = centerGrid ? new Vector2(-gridSize.x / 2f, -gridSize.y / 2f) : Vector2.zero;
        Vector2 totalOffset = centerOffset + manualOffset;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject cellObj = Instantiate(cellPrefab, parentContainer);

                RectTransform rect = cellObj.GetComponent<RectTransform>();
                if (rect == null)
                    rect = cellObj.AddComponent<RectTransform>();

                rect.anchorMin = new Vector2(0.5f, 0.5f);
                rect.anchorMax = new Vector2(0.5f, 0.5f);
                rect.pivot = Vector2.zero;

                rect.anchoredPosition = new Vector2(x * cellSize, y * cellSize) + totalOffset;
                rect.sizeDelta = new Vector2(cellSize, cellSize);

                Tile tile = cellObj.GetComponent<Tile>();
                TileType tileType = GetNonMatchingType(x, y);
                tile.Initialize(x, y, tileType);

                _tiles[x, y] = tile;
                _grid.SetValue(x, y, (int)tileType);
            }
        }
    }
    
    private TileType GetNonMatchingType(int x, int y)
    {
        List<TileType> validTypes = new List<TileType>((TileType[])System.Enum.GetValues(typeof(TileType)));

        if (x >= 2)
        {
            TileType leftOne = _tiles[x - 1, y].Type;
            TileType leftTwo = _tiles[x - 2, y].Type;

            if (leftOne == leftTwo)
                validTypes.Remove(leftOne);
        }

        if (y >= 2)
        {
            TileType belowOne = _tiles[x, y - 1].Type;
            TileType belowTwo = _tiles[x, y - 2].Type;

            if (belowOne == belowTwo)
                validTypes.Remove(belowOne);
        }
        return validTypes[Random.Range(0, validTypes.Count)];
    }
    
    public Tile GetTile(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
            return _tiles[x, y];

        return null;
    }
}