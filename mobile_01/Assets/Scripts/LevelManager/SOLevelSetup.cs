using UnityEngine;

[CreateAssetMenu(fileName = "SO_Level_", menuName = "Game/Level Setup")]
public class SOLevelSetup : ScriptableObject
{
    public ArtManager.ArtType artType;

    [Header("Level Sections")]
    public int numberOfStartSections = 1;
    public int numberOfSections = 5;
    public int numberOfFinishSections = 1;

}
