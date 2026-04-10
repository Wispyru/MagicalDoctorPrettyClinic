using UnityEngine;

public class MedicineData : MonoBehaviour
{
    //public variables
    public MedicineType Type;

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
                spriteRenderer.color = Color.red;
                break;
            case MedicineType.MedicineType2:
                spriteRenderer.color = Color.green;
                break;
            case MedicineType.MedicineType3:
                spriteRenderer.color = Color.blue;
                break;
            case MedicineType.MedicineType4:
                spriteRenderer.color = Color.yellow;
                break;
            case MedicineType.MedicineType5:
                spriteRenderer.color = Color.purple;
                break;
        }
    }
}
