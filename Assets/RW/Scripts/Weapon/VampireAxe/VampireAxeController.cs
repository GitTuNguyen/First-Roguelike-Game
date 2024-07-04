using UnityEngine;
using System.Collections;
using Unity.Collections;
using Unity.Mathematics;

public class VampireAxeController : WeaponController
{    
    private bool isSpawned = false;
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (isSpawned)
        {
            StopCoroutine(AttackRoutine());
        }
    }
       
    private Vector3 PolarToCartesian(float degrees)
    {
        float theta = degrees * Mathf.Deg2Rad;
        float x = radius * Mathf.Cos(theta);
        float y = radius * Mathf.Sin(theta);
        return new Vector3(x, y, 0.0f);
    }
    protected override void Attack()
    { 
        var projectile = Instantiate(prefab, projectileSpawnPosition, Quaternion.identity, player.transform);
        
        projectileList.Add(projectile);
    }
    protected override IEnumerator AttackRoutine()
    {
        while (!GameStateManager.Instance.isGameOver)
        {
            if (projectileList.Count == 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    projectileSpawnPosition = player.transform.position + PolarToCartesian(i * 360 / amount);
                    Attack();
                }
                if (level == maxLevel)
                {
                    isSpawned = true;
                }
                yield return new WaitForSeconds(cooldown);
            }
            yield return null;
        }          
    }
}

