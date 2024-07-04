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
        if (level < maxLevel)
        {
            while (!GameStateManager.Instance.isGameOver)
            {
                for (int i = 0; i < amount; i++)
                {
                    projectileSpawnPosition = player.transform.position + PolarToCartesian(i * 360 / amount);
                    Attack();
                    yield return new WaitForSeconds(projectileInterval);
                }            
                yield return new WaitForSeconds(cooldown);
            }
        } else if (!isSpawned)
        {
            for (int i = 0; i < amount; i++)
            {
                projectileSpawnPosition = player.transform.position + PolarToCartesian(i * 360 / amount);
                Attack();
                yield return new WaitForSeconds(projectileInterval);
            }
            isSpawned = true;
        }        
    }
}

