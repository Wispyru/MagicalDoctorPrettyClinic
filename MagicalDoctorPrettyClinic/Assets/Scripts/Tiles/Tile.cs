using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public TileType Type { get; private set; }
    public bool IsSelected { get; private set; }

    private RectTransform _rectTransform;
    private Image _image;
    
    public void Initialize(int x, int y, TileType type)
    {
        X = x;
        Y = y;
        Type = type;

        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        UpdateVisual();
    }
    
    public void SetSelected(bool isSelected)
    {
        IsSelected = isSelected;
        UpdateVisual();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        SetSelected(!IsSelected);
    }
    
    private void UpdateVisual()
    {
        if (_image == null) return;

        _image.color = IsSelected ? Color.yellow : Color.white;
    }
}