using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.EditorTools;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/New Player Upgrade")]
public class PlayerUpgrade : ScriptableObject
{
    [Header("Upgrade Variables")]
    public string upgradeName;
    public string description;

    public Image upgradeImage;

    public string affectedVariable;
    public string affectedScript;

    [Header("Only have one set to true, additive or multiplicative. Not Both")]
    public bool additive;
    public int additiveAmount;
    public bool multiplicative;
    public float multiplicativeAmount;


}
