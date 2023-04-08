using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<WeaponStats> stats;
    public string projectileName;
    public Sprite weaponSprite;
    public GameObject prefab;
    public int level;
    public int maxLevel;
    public int dame;
    public float speed;
    public int pierce;
    public int amount;
    public float projectileInterval;
    public float projectileScale;
    public float attackDuration;
    public float cooldown;
    public float timeToDestroy;
    public Vector3 projectileSpawnPosition;
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
        pierce = stats[level - 1].pierce;
        amount = stats[level - 1].amount;
        projectileInterval = stats[level - 1].projectileInterval;
        projectileScale = stats[level - 1].projectileScale;
        attackDuration = stats[level - 1].attackDuration;
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
        Instantiate(prefab, projectileSpawnPosition, Quaternion.identity);
    }

    protected virtual IEnumerator AttackRoutine()
    {
        while (!GameStateManager.Instance.isGameOver)
        {
            for (int i = 0; i < amount; i++)
            {
                Attack();
                yield return new WaitForSeconds(projectileInterval);
            }
            yield return new WaitForSeconds(cooldown);
        }
    }
}
