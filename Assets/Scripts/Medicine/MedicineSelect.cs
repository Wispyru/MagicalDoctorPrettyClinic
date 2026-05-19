using UnityEngine;

public class MedicineSelect : MonoBehaviour
{
    private Vector3 _originalScale;
    private Vector3 _selectedScale;
    private SpriteRenderer _renderer;
    private int _originalSortingOrder;

    public bool Swapable = true;
    public Vector2Int Position;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _originalScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
        _selectedScale = new Vector3(0.6f, 0.6f, 1);
    }

    public void Select()
    {
        Swapable = false;
        GameData.SelectedTile = this;
        transform.localScale = _selectedScale;
        _originalSortingOrder = _renderer.sortingOrder;
        _renderer.sortingOrder = 100;
    }

    public void Unselect()
    {
        Swapable = true;
        GameData.SelectedTile = null;
        transform.localScale = _originalScale;
        _renderer.sortingOrder = _originalSortingOrder;
    }
}