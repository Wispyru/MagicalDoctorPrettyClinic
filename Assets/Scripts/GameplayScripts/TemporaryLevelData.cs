// LevelData.cs
using UnityEngine;

[System.Serializable]
public class TemporaryLevelData
{
    public string LevelName;
    [TextArea] public string Description;  // TextArea gives a bigger text box in the Inspector
    public int MinimumScore;
    public int MaximumScore;
}