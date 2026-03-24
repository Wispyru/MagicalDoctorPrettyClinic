using UnityEngine;
using UnityEngine.UI;

public class GridTest : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float cellSize = 50f;

    public Canvas canvas;
    public RectTransform parentContainer;
    public GameObject cellPrefab;

    public bool centerGrid = true;
    public Vector2 manualOffset;

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
                GameObject cellObj = Instantiate(cellPrefab, parentContainer);

                RectTransform rect = cellObj.GetComponent<RectTransform>();
                if (rect == null)
                    rect = cellObj.AddComponent<RectTransform>();

                rect.anchorMin = new Vector2(0.5f, 0.5f);
                rect.anchorMax = new Vector2(0.5f, 0.5f);
                rect.pivot = Vector2.zero;

                rect.anchoredPosition = new Vector2(x * cellSize, y * cellSize) + totalOffset;
                rect.sizeDelta = new Vector2(cellSize, cellSize);
            }
        }
    }
}