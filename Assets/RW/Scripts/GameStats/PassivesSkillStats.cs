using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Passive Skill Stats")]
public class PassivesSkillStats : SkillStats
{
    public float reduceCooldown;
    public float increaseDame;
    public float armor;
    public float amountProjectile;
    public float moveSpeed;
    public float projectileSpeed;
    public float maxHealth;
    public float bonusExperience;
}
