using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireballController : WeaponController
{

    public int amount;
    public float fireballInterval;
    private Player player;
    private EnemySpawner enemySpawner;
    protected override void Start()
    {
        player = FindObjectOfType<Player>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        base.Start();        
    }

    protected override void SetStats(int level)
    {
        base.SetStats(level);
        amount = stats[level - 1].amount;
        fireballInterval = stats[level - 1].projectileInterval;
    }

    protected override void Attack()
    {        
        Instantiate(prefab, player.transform.position, Quaternion.identity);
        AudioManager.Instance.PlaySFX("FireBall");
    }

    protected override IEnumerator AttackRoutine()
    {
        while (!GameStateManager.Instance.isGameOver)
        {            
            if (enemySpawner.enemyList.Count > 0)
            {                
                for (int i = 0; i < amount; i++)                
                {
                    Attack();
                    yield return new WaitForSeconds(fireballInterval);
                }
            }            
            yield return new WaitForSeconds(cooldown);
        }
    }
}
