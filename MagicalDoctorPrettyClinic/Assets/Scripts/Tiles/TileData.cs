using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "Match3/TileData")]
public class TileData : ScriptableObject
{
    public TileType Type;
    public Sprite Icon;
    // Illness weakness/strength data can be added here later
}