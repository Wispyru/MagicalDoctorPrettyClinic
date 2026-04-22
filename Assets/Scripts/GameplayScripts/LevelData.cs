using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Levels/DataObject")]
public class LevelData : ScriptableObject
{
    public float MaxTimeInSeconds;
    public int MaximumMoves;
    public int PointsNeeded;
    
    
}
