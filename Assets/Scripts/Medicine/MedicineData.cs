using UnityEngine;

public class MedicineData : MonoBehaviour
{
    //public variables
    // quick fix comment
    public MedicineType Type;
    public Sprite[] sprites;
    public int Points;
    
    /// <summary>
    /// Sets the color of the sprite based on the medicine type
    /// </summary>
    public void SetMedicineColor()
    {
        //TODO: Change this to the sprites when the time comes.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        switch (Type)
        {
            case MedicineType.MedicineType1:
                spriteRenderer.sprite = sprites[0];
                break;
            case MedicineType.MedicineType2:
                spriteRenderer.sprite = sprites[1];
                break;
            case MedicineType.MedicineType3:
                spriteRenderer.sprite = sprites[2];
                break;
            case MedicineType.MedicineType4:
                spriteRenderer.sprite = sprites[3];
                break;
            case MedicineType.MedicineType5:
                spriteRenderer.sprite = sprites[4];
                break;
        }
    }
}
