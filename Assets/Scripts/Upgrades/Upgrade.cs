using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrades/Upgrade")]
public class Upgrade : ScriptableObject
{
    public string upgradeName;
    public string description;

    public enum UpgradeType { DodgeSpeed, InvincibilityDuration, ExtraHealth }
    public UpgradeType type;

    public float value;
}


