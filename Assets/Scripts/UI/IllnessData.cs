using UnityEngine;

[System.Serializable]
public class IllnessData
{
    public string IllnessName;
    [TextArea] public string Description;
    public Sprite LockedSprite;    // Silhouette version
    public Sprite UnlockedSprite;  // Colored version
    public bool IsUnlocked;        // TODO: Replace with data system later
}