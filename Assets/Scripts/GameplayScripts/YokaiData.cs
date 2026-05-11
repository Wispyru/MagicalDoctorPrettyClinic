using UnityEngine;

[CreateAssetMenu(fileName = "New Yokai", menuName = "Gameplay/Yokai Data")]
public class YokaiData : ScriptableObject
{
    public string yokaiName;
    public MedicineType yokaiWeakness;
    public int yokaiHealth = 10;
    
    public Sprite yokaiSprite;
    public GameObject yokaiPrefab;

    public string yokaiDescription;
    
}
