using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MagicCircleController : WeaponController
{
    
    private GameObject magicCircle;
    private Player player;
    public float attackDuration;
    protected override void Start()
    {
        player = FindObjectOfType<Player>();
        magicCircle = Instantiate(prefab, player.transform.position, Quaternion.identity, player.transform);        
        base.Start();
    }


    protected override void SetStats(int level)
    {
        base.SetStats(level);
        scale = stats[level - 1].projectileScale;
    }

    protected override IEnumerator AttackRoutine()
    {
        while (!GameStateManager.Instance.isGameOver)
        {
            Debug.Log("circle start attack ");
            Attack();
            yield return new WaitForSeconds(cooldown);
        }
    }
}
