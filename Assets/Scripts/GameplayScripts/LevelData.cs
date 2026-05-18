using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/DataObject")]
public class LevelData : ScriptableObject
{
    public int Rounds;
    public int MovesPerRound;
    public float MaxTimeInSeconds;
    public int RequiredPoints;
    [TextArea] public string Description;


    // assigned animations
    public VideoClip StartingAnimation;
    public VideoClip IdleAnimation;
    public VideoClip AttackAnimation;
    public VideoClip WinAnimation;
    public VideoClip LoseAnimation;
}
