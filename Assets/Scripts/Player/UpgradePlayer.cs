using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.EditorTools;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/New Player Upgrade")]
public class PlayerUpgrade : ScriptableObject
{
    [Header("Upgrade Variables")]
    [Tooltip("Upgrade Type:\n1: Additive \n2: Multiplicative \n3: Enable Bool")]
    [Range(1,3)]
    public int upgradeType;
    public string upgradeName;
    public string description;

    public Sprite upgradeSprite;

    public string affectedVariable;

    public float amount;


}
