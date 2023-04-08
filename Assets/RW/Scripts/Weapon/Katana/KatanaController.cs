using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaController : WeaponController
{
    private Player player;
    private PlayerController playerController;
    public List<Transform> spawnPosition;
    private int spawnPositionIndex;
    private int i;
    protected override void Start()
    {
        player = FindObjectOfType<Player>();
        playerController = FindObjectOfType<PlayerController>();
        transform.parent = player.transform;
        spawnPositionIndex = 0;
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

    protected override void Attack()
    {
        projectileSpawnPosition = spawnPosition[spawnPositionIndex].transform.position;
        Instantiate(prefab, projectileSpawnPosition, spawnPosition[spawnPositionIndex].transform.rotation, spawnPosition[spawnPositionIndex].transform);
        spawnPositionIndex++;
        if (spawnPositionIndex == spawnPosition.Count || i == amount - 1)
        {
            spawnPositionIndex = 0;
        }
    }

}
