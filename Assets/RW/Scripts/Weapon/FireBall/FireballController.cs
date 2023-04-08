using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireballController : WeaponController
{
    private Player player;
    private EnemySpawner enemySpawner;
    protected override void Start()
    {
        player = FindObjectOfType<Player>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        base.Start();        
    }


    protected override void Attack()
    {
        projectileSpawnPosition = player.transform.position;
        if (enemySpawner.enemyList.Count > 0)
        {
            base.Attack();
        }
        AudioManager.Instance.PlaySFX("FireBall");
    }
        
}
