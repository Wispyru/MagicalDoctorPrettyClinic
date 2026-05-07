using UnityEngine;

[System.Serializable]
public class IllnessData
{
    public string IllnessName;
    [TextArea] public string Description;
    public Sprite LockedSprite;    // Black silhouette version
    public Sprite UnlockedSprite;  // Full colored version
    public bool IsUnlocked;        // TODO: Replace with team's data system later
}