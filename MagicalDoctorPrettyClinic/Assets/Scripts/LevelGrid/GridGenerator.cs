using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float cellSize = 50f;

    public RectTransform parentContainer;
    public GameObject cellPrefab;

    public bool centerGrid = true;
    public Vector2 manualOffset;

    public TileType[] possibleTiles;

    private Grid grid;

    void Start()
    {
        grid = new Grid(width, height, cellSize);
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
                TileType tileType = GetSafeRandomTile(x, y);

                TileData tileData = new TileData(tileType, new Vector2Int(x, y));
                grid.SetTile(x, y, tileData);

                GameObject cellObj = Instantiate(cellPrefab, parentContainer);

                RectTransform rect = cellObj.GetComponent<RectTransform>();
                if (rect == null)
                    rect = cellObj.AddComponent<RectTransform>();

                rect.anchorMin = new Vector2(0.5f, 0.5f);
                rect.anchorMax = new Vector2(0.5f, 0.5f);
                rect.pivot = Vector2.zero;

                rect.anchoredPosition = new Vector2(x * cellSize, y * cellSize) + totalOffset;
                rect.sizeDelta = new Vector2(cellSize, cellSize);

                TileVisualiser(cellObj, tileType);
            }
        }
    }
    
    private TileType GetSafeRandomTile(int x, int y)
    {
        List<TileType> availableTiles = new List<TileType>(possibleTiles);
        
        if (x >= 2)
        {
            TileType left1 = grid.GetTile(x - 1, y).type;
            TileType left2 = grid.GetTile(x - 2, y).type;

            if (left1 == left2)
            {
                availableTiles.Remove(left1);
            }
        }
        
        if (y >= 2)
        {
            TileType down1 = grid.GetTile(x, y - 1).type;
            TileType down2 = grid.GetTile(x, y - 2).type;

            if (down1 == down2)
            {
                availableTiles.Remove(down1);
            }
        }

        return availableTiles[Random.Range(0, availableTiles.Count)];
    }
    
    private void TileVisualiser(GameObject tile, TileType type)
    {
        Image img = tile.GetComponent<Image>();

        switch (type)
        {
            case TileType.Cure1:
                img.color = Color.red;
                break;
            case TileType.Cure2:
                img.color = Color.blue;
                break;
            case TileType.Cure3:
                img.color = Color.green;
                break;
            case TileType.Cure4:
                img.color = Color.yellow;
                break;
            case TileType.Cure5:
                img.color = new Color(0.5f, 0f, 0.5f);
                break;
        }
    }
}