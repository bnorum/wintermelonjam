using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.EditorTools;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/New Weapon Upgrade")]
public class WeaponUpgrade : ScriptableObject
{
    [Header("Upgrade Variables")]
    [Range(1,3)]
    [Tooltip("1 = boomerang \n2 = TriangleGun \n3 = currently nothing")]
    public int weapon;
    public string upgradeName;
    public string description;

    public Sprite upgradeImage;

    public string affectedVariable;
    public string affectedScript;

    [Header("Only have one set to true, additive or multiplicative. Not Both")]
    public bool additive;
    public int additiveAmount;
    public bool multiplicative;
    public float multiplicativeAmount;


}
