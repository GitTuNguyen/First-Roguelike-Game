using UnityEngine;


[CreateAssetMenu(fileName = "Weapon Stats")]
public class WeaponStats : SkillStats
{
    public int projectileDMG;
    public float projectileSpeed;
    public int pierce;
    public int amount;
    public float projectileInterval;
    public float projectileScale;
    public float radius;
    public float attackDuration;
    public float cooldown;
    public float timeToDestroy;
}
