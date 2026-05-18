using UnityEngine;

public class MedicineData : MonoBehaviour
{
    public MedicineType Type;
    public Sprite[] sprites;
    public int Points;

    /// <summary>
    /// Sets the color of the sprite based on the medicine type
    /// </summary>
    public void SetMedicineColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        switch (Type)
        {
            case MedicineType.Bandaid:
                spriteRenderer.sprite = sprites[0];
                break;
            case MedicineType.EHBO_Kit:
                spriteRenderer.sprite = sprites[1];
                break;
            case MedicineType.IV_Bag:
                spriteRenderer.sprite = sprites[2];
                break;
            case MedicineType.Needle:
                spriteRenderer.sprite = sprites[3];
                break;
            case MedicineType.Pill:
                spriteRenderer.sprite = sprites[4];
                break;
        }
    }
}