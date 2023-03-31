using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightSpearController : WeaponController
{
    public int amount;
    public float lightSpearInterval;
    private Player player;
    protected override void Start()
    {
        player = FindObjectOfType<Player>();
        base.Start();
    }

    protected override void SetStats(int level)
    {
        base.SetStats(level);
        amount = stats[level - 1].amount;
        lightSpearInterval = stats[level - 1].projectileInterval;
    }

    protected override void Attack()
    {        
        Instantiate(prefab, player.transform.position, Quaternion.identity);
        AudioManager.Instance.PlaySFX("LightSpear");
    }

    protected override IEnumerator AttackRoutine()
    {
        while (!GameStateManager.Instance.isGameOver)
        {
            for (int i = 0; i < amount; i++)
            {
                Attack();
                yield return new WaitForSeconds(lightSpearInterval);
            }
            yield return new WaitForSeconds(cooldown);
        }
    }
}
