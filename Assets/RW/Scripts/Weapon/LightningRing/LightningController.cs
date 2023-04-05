using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningController : WeaponController
{
    public int amount;
    public float lightningInterval;
    private EnemySpawner enemySpawner;
    protected override void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        base.Start();
    }

    protected override void SetStats(int level)
    {
        base.SetStats(level);
        amount = stats[level - 1].amount;
        lightningInterval = stats[level - 1].projectileInterval;
        scale = stats[level - 1].projectileScale;
    }

    protected override void Attack()
    {
        if (enemySpawner.enemyList.Count > 0)
        {
            GameObject randomEnemy = enemySpawner.enemyList[Random.Range(0, enemySpawner.enemyList.Count)];
            Instantiate(prefab, randomEnemy.transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySFX("Lightning");
        }    
    }

    protected override IEnumerator AttackRoutine()
    {
        while (!GameStateManager.Instance.isGameOver)
        {            
            for (int i = 0; i < amount; i++)
            {
                Attack();
                yield return new WaitForSeconds(lightningInterval);
            }
            yield return new WaitForSeconds(cooldown);
        }
    }
}
