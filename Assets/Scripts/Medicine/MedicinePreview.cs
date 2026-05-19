using UnityEngine;

public class MedicinePreview : MonoBehaviour
{
    private MedicineSelect _previewTile;

    public void UpdatePreview(MedicineSelect newTarget, Vector3 snapPosition)
    {
        if (newTarget == _previewTile) return;

        if (_previewTile != null)
        {
            _previewTile.transform.position = _previewTile.GetComponent<MedicineDrag>().OriginalPosition;
            _previewTile = null;
        }

        if (newTarget == null) return;

        _previewTile = newTarget;
        _previewTile.transform.position = snapPosition;
    }

    public void ClearPreview()
    {
        if (_previewTile == null) return;
        _previewTile.transform.position = _previewTile.GetComponent<MedicineDrag>().OriginalPosition;
        _previewTile = null;
    }
}