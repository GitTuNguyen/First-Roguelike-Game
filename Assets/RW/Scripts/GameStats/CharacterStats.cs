using UnityEngine;
[CreateAssetMenu(fileName = "Character Stats")]
public class CharacterStats : ScriptableObject
{
    public string characterName;
    public WeaponController defaultWeapon;
    public float defaultpickUpRadius;

    public float defaultSpeed;

    public float defaultMaxHealth;
    public float healthForLevelUpStep;

    public float defaultMaxExp;
    public float expForLevelUpStep;
    public string characterInfo;
}
