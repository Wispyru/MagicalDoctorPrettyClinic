using Unity.VisualScripting;
using UnityEngine;

public class MedicineSelect : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Color _originalColor;
    private GridTileSwapping _tileSwapping;

    public Vector2Int Position;
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _tileSwapping = GetComponent<GridTileSwapping>();
        _originalColor = _renderer.color;

    }

    public void Unselect()
    {
        GameData.SelectedTile = null;
        _renderer.color = _originalColor;
    }
    public void Select()
    {
        _renderer.color = Color.white;
        GameData.SelectedTile = this;
    }

    private void OnMouseDown()
    {
        if (GameData.SelectedTile == null)
        {
            Select();
            return;
        }

        if (GameData.SelectedTile == this)
        {
            Unselect();
            return;
        }

        // A different tile is already selected
        if (Vector2Int.Distance(GameData.SelectedTile.Position, Position) == 1)
        {
            _tileSwapping.SwapTiles(Position, GameData.SelectedTile.Position);
            GameData.SelectedTile.Unselect();
        }
        else
        {
            GameData.SelectedTile.Unselect();
            Select();
        }
    }
}
