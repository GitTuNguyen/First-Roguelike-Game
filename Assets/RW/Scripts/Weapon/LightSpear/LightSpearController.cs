using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightSpearController : WeaponController
{
    private Player player;
    protected override void Start()
    {
        player = FindObjectOfType<Player>();
        base.Start();
    }

    protected override void Attack()
    {
        projectileSpawnPosition = player.transform.position;
        base.Attack();
        AudioManager.Instance.PlaySFX("LightSpear");
    }
        
}
