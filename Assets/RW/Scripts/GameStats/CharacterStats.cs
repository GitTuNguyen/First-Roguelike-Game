using UnityEngine;
[CreateAssetMenu(fileName = "Character Stats")]
public class CharacterStats : ScriptableObject
{
    public string characterName;
    public WeaponController defaultWeapon;
    public float defaultPickUpRadius;
    public float defaultWeaponSize;
    public float defaultSpeed;
    public float defaultDodgeRate;
    public float defaultMaxHealth;
    public float healthForLevelUpStep;
    public float defaultMaxExp;
    public float expForLevelUpStep;
    public float defaultArmor;
    public string characterInfo;

    [Header("Passive bonus")]
    public int bonusAmountProjectile;
    public float bonusDodgeRate;
    public float bonusSpeed;
    public float bonusMaxHealth;
    public float bonusPickUpRadius;
    public float bonusWeaponSize;
    public float bonusHealing;
    public float bonusArmor;
}
