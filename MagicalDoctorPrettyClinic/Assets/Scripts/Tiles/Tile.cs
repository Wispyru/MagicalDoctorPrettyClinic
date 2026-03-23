using UnityEngine;

public class Tile : MonoBehaviour
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public TileData Data { get; private set; }

    [SerializeField] private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// Initializes the tile with its grid position and TileData.
    /// </summary>
    public void Initialize(int x, int y, TileData data)
    {
        X = x;
        Y = y;
        Data = data;

        UpdateVisual();
    }

    /// <summary>
    /// Updates the visual of the tile using its TileData icon.
    /// </summary>
    private void UpdateVisual()
    {
        if (_spriteRenderer != null && Data.Icon != null)
            _spriteRenderer.sprite = Data.Icon;
    }
}