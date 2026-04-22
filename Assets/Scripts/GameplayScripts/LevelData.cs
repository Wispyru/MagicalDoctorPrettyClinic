using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
public class LevelData : ScriptableObject
{
    public float MaxTime;
    public int MaxMoves;
    public float ComboWindow;
}