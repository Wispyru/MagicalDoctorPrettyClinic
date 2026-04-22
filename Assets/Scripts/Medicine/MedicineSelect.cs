using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MedicineSelect : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Vector3 _originalScale;
    private Vector3 _selectedScale;
    private GridTileSwapping _tileSwapping;

    public bool Swapable = true;
    public Vector2Int Position;
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _tileSwapping = GetComponent<GridTileSwapping>();
        _originalScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, 1);
        _selectedScale = new Vector3(0.55f, 0.55f, 1);

    }

    public void Unselect()
    {
        Swapable = true;
        GameData.SelectedTile = null;
        gameObject.transform.localScale = _originalScale;
    }
    public void Select()
    {
        GameData.SelectedTile = this;
        gameObject.transform.localScale = _selectedScale;
    }

    private void OnMouseDown()
    {
        if (GameData.IsAnimating) return;

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

        if (Vector2Int.Distance(GameData.SelectedTile.Position, Position) == 1)
        {
            _tileSwapping.SwapTiles(GameData.SelectedTile.Position, Position);
            GameData.SelectedTile.Unselect();
        }
        else
        {
            GameData.SelectedTile.Unselect();
            Select();
        }
    }
    
}
