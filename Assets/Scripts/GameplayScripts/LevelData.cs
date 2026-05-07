using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/DataObject")]
public class LevelData : ScriptableObject
{
    public int Rounds;
    public int MovesPerRound;
    public int MaxTimeInSeconds;
    public int RequiredPoints;
}
