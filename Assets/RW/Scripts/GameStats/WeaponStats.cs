using UnityEngine;


[CreateAssetMenu(fileName = "Projectile Stats")]
public class WeaponStats : ScriptableObject
{
    public Sprite sprite;
    public int projectileLevel;
    public int maxLevel;
    public int projectileDMG;
    public float projectileSpeed;
    public int pierce;
    public int amount;
    public float projectileInterval;
    public float projectileScale;
    public float attackDuration;
    public float cooldown;
    public float timeToDestroy;
    public string description;
}
