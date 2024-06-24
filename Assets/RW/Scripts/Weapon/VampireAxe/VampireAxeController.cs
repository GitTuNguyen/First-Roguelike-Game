using UnityEngine;
using System.Collections;
using Unity.Collections;
using Unity.Mathematics;

public class VampireAxeController : WeaponController
{    
    private Player player;
    private bool isSpawned = false;
    protected override void Start()
    {
        player = FindObjectOfType<Player>();
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
        //projectileSpawnPosition = player.transform.position + new Vector3(0, Random.Range(-0.25f, 0.25f), 0);
        Instantiate(prefab, projectileSpawnPosition, Quaternion.identity, player.transform);
        //AudioManager.Instance.PlaySFX("Arrow");
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

