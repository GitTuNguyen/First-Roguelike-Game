using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningController : WeaponController
{
    private EnemySpawner enemySpawner;
    protected override void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        base.Start();
    }

    protected override void Attack()
    {
        if (enemySpawner.enemyList.Count > 0)
        {
            GameObject randomEnemy = enemySpawner.enemyList[Random.Range(0, enemySpawner.enemyList.Count)];
            projectileSpawnPosition = randomEnemy.transform.position;
            base.Attack();
            AudioManager.Instance.PlaySFX("Lightning");
        }    
    }
}
