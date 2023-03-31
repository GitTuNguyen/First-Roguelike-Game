using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<WeaponStats> stats;
    public Sprite weaponSprite;
    public GameObject prefab;
    public int level;
    public float speed;
    public int dame;
    public float timeToDestroy;
    public float scale;
    public float cooldown;
    [HideInInspector]
    public int maxLevel;
    protected virtual void Start()
    {
        level = 1;
        maxLevel = stats.Count;
        SetStats(level);
        StartCoroutine(AttackRoutine());
    }

    protected virtual void SetStats(int level)
    {
        dame = stats[level - 1].projectileDMG;
        speed = stats[level - 1].projectileSpeed;
        cooldown = stats[level - 1].cooldown;
        timeToDestroy = stats[level - 1].timeToDestroy;
    }

    public string getDescriptionNextLevel()
    {
        return stats[level].description;
    }

    public bool isCanUplevel()
    {
        return level < maxLevel;
    }
    public void Upgrade()
    {
        level++;
        SetStats(level);
    }

    protected virtual void Attack()
    {

    }

    protected virtual IEnumerator AttackRoutine()
    {
        yield return null;
    }
}
