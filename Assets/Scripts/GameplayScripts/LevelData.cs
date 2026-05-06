using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/DataObject")]
public class LevelData : ScriptableObject
{
    public float MaxTime;
    public float ComboWindow;
    public int MaxMoves;
}