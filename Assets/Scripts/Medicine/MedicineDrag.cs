using UnityEngine;

public class MedicineDrag : MonoBehaviour
{
    private GridTileSwapping _tileSwapping;
    private MedicineSelect _select;
    private MedicinePreview _preview;

    private Vector3 _originalPosition;
    private Vector3 _dragStartWorldPos;
    private bool _isDragging = false;
    private float _tileSize;

    [Range(0.5f, 2f)]
    public float DragRangeMultiplier = 1f;
    private const float _dragThreshold = 0.2f;

    public Vector3 OriginalPosition => _originalPosition;

    private void Start()
    {
        _tileSwapping = FindAnyObjectByType<GridTileSwapping>();
        _select = GetComponent<MedicineSelect>();
        _preview = GetComponent<MedicinePreview>();
        _originalPosition = transform.position;
        _tileSize = GetGridSpacing();
    }

    public void ForceOriginalPosition(Vector3 pos)
    {
        _originalPosition = pos;
    }

    public Vector3 GetAuthorativePosition()
    {
        GridGeneration grid = FindAnyObjectByType<GridGeneration>();
        if (grid != null)
            return grid.GetWorldPosition(_select.Position.x, _select.Position.y);
        return _originalPosition;
    }

    public void ResetToCurrentPosition()
    {
        GridGeneration grid = FindAnyObjectByType<GridGeneration>();
        if (grid != null)
            _originalPosition = grid.GetWorldPosition(_select.Position.x, _select.Position.y);
        else
            _originalPosition = transform.position;

        transform.position = _originalPosition;
        _tileSize = GetGridSpacing();
    }

    private void OnMouseDown()
    {
        if (GameData.IsAnimating) return;

        _isDragging = false;
        _dragStartWorldPos = GetMouseWorldPos();

        GridGeneration grid = FindAnyObjectByType<GridGeneration>();
        if (grid != null)
            _originalPosition = grid.GetWorldPosition(_select.Position.x, _select.Position.y);
        else
            _originalPosition = transform.position;

        _tileSize = GetGridSpacing();
    }

    private void OnMouseDrag()
    {
        if (GameData.IsAnimating) return;

        Vector3 currentMousePos = GetMouseWorldPos();
        float dragDistance = Vector3.Distance(_dragStartWorldPos, currentMousePos);

        if (!_isDragging && dragDistance >= _dragThreshold)
        {
            _isDragging = true;

            if (GameData.SelectedTile != null && GameData.SelectedTile != _select)
                GameData.SelectedTile.GetComponent<MedicineDrag>().CancelDrag();

            _select.Select();
        }

        if (_isDragging)
        {
            Vector3 clamped = ClampedPosition(currentMousePos);
            transform.position = new Vector3(clamped.x, clamped.y, _originalPosition.z);
            _preview.UpdatePreview(GetTileInDragDirection(), _originalPosition);
        }
    }

    private void OnMouseUp()
    {
        if (GameData.IsAnimating) return;

        if (!_isDragging)
        {
            HandleClick();
            return;
        }

        _isDragging = false;

        MedicineSelect targetTile = GetTileInDragDirection();

        if (targetTile == null)
        {
            _preview.ClearPreview();
            CancelDrag();
            return;
        }

        _preview.ClearPreview();
        transform.position = _originalPosition;
        _select.Unselect();
        _tileSwapping.SwapTiles(_select.Position, targetTile.Position);
    }

    public void CancelDrag()
    {
        _isDragging = false;
        transform.position = _originalPosition;
        _select.Unselect();
        _preview.ClearPreview();
    }

    private void HandleClick()
    {
        if (GameData.SelectedTile == null)
        {
            _select.Select();
            return;
        }

        if (GameData.SelectedTile == _select)
        {
            _select.Unselect();
            return;
        }

        MedicineSelect selected = GameData.SelectedTile;
        Vector2Int delta = selected.Position - _select.Position;
        if (Mathf.Abs(delta.x) + Mathf.Abs(delta.y) == 1)
        {
            _tileSwapping.SwapTiles(selected.Position, _select.Position);
            selected.Unselect();
        }
        else
        {
            selected.Unselect();
            _select.Select();
        }
    }

    private Vector3 ClampedPosition(Vector3 mouseWorldPos)
    {
        Vector2 offset = new Vector2(
            mouseWorldPos.x - _originalPosition.x,
            mouseWorldPos.y - _originalPosition.y
        );

        if (Mathf.Abs(offset.x) >= Mathf.Abs(offset.y))
            offset.y = 0f;
        else
            offset.x = 0f;

        offset.x = Mathf.Clamp(offset.x, -_tileSize * DragRangeMultiplier, _tileSize * DragRangeMultiplier);
        offset.y = Mathf.Clamp(offset.y, -_tileSize * DragRangeMultiplier, _tileSize * DragRangeMultiplier);

        return new Vector3(
            _originalPosition.x + offset.x,
            _originalPosition.y + offset.y,
            _originalPosition.z
        );
    }

    private MedicineSelect GetTileInDragDirection()
    {
        Vector2 offset = new Vector2(
            transform.position.x - _originalPosition.x,
            transform.position.y - _originalPosition.y
        );

        if (offset.magnitude < _tileSize * DragRangeMultiplier * 0.5f) return null;

        Vector2Int gridDirection;
        if (Mathf.Abs(offset.x) >= Mathf.Abs(offset.y))
            gridDirection = offset.x > 0 ? Vector2Int.right : Vector2Int.left;
        else
            gridDirection = offset.y > 0 ? Vector2Int.up : Vector2Int.down;

        Vector2Int targetGridPos = _select.Position + gridDirection;

        foreach (MedicineSelect tile in FindObjectsByType<MedicineSelect>(FindObjectsInactive.Exclude))
        {
            if (tile != _select && tile.Position == targetGridPos)
                return tile;
        }

        return null;
    }

    private float GetGridSpacing()
    {
        foreach (MedicineSelect tile in FindObjectsByType<MedicineSelect>(FindObjectsInactive.Exclude))
        {
            if (tile == _select) continue;
            Vector2Int delta = tile.Position - _select.Position;
            if (Mathf.Abs(delta.x) + Mathf.Abs(delta.y) == 1)
                return Vector3.Distance(transform.position, tile.transform.position);
        }
        return GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}