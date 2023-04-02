using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaController : WeaponController
{
    private Player player;
    private PlayerController playerController;
    public int amount;
    public float katanaInterval;
    public List<Transform> spawnPosition;
    private int spawnPositionIndex;
    protected override void Start()
    {
        player = FindObjectOfType<Player>();
        playerController = FindObjectOfType<PlayerController>();
        Debug.Log("spawnPosition.count = " + spawnPosition.Count);
        transform.parent = player.transform;
        base.Start();
    }

    private void Update()
    {
        if (playerController.moveDir.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        } else if (playerController.moveDir.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected override void SetStats(int level)
    {
        base.SetStats(level);
        scale = stats[level - 1].projectileScale;
        amount = stats[level - 1].amount;
        katanaInterval = stats[level - 1].projectileInterval;
    }

    protected override void Attack()
    {
        Instantiate(prefab, spawnPosition[spawnPositionIndex].transform.position, spawnPosition[spawnPositionIndex].transform.rotation, spawnPosition[spawnPositionIndex].transform);        
    }

    protected override IEnumerator AttackRoutine()
    {
        while (!GameStateManager.Instance.isGameOver)
        {
            spawnPositionIndex = 0;
            for (int i = 0; i < amount; i++)
            {                
                Attack();
                spawnPositionIndex++;
                if (spawnPositionIndex >= spawnPosition.Count)
                {
                    spawnPositionIndex = 0;
                } 
                yield return new WaitForSeconds(katanaInterval);
            }
            yield return new WaitForSeconds(cooldown);
        }
    }
}
