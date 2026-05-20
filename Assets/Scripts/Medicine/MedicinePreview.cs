using UnityEngine;

public class MedicinePreview : MonoBehaviour
{
    private MedicineSelect _previewTile;

    public void UpdatePreview(MedicineSelect newTarget, Vector3 snapPosition)
    {
        if (newTarget == _previewTile) return;

        if (_previewTile != null)
        {
            MedicineDrag drag = _previewTile.GetComponent<MedicineDrag>();
            if (drag != null)
            {
                // Use grid-authoritative position, not cached value
                Vector3 truePos = drag.GetAuthorativePosition();
                _previewTile.transform.position = truePos;
                drag.ForceOriginalPosition(truePos);
            }
            _previewTile = null;
        }

        if (newTarget == null) return;

        MedicineDrag newDrag = newTarget.GetComponent<MedicineDrag>();
        if (newDrag == null) return;

        _previewTile = newTarget;
        _previewTile.transform.position = snapPosition;
    }

    public void ClearPreview()
    {
        if (_previewTile == null) return;

        MedicineDrag drag = _previewTile.GetComponent<MedicineDrag>();
        if (drag != null)
        {
            Vector3 truePos = drag.GetAuthorativePosition();
            _previewTile.transform.position = truePos;
            drag.ForceOriginalPosition(truePos);
        }

        _previewTile = null;
    }
}