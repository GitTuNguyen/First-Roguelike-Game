using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : WeaponController
{
    public int amount;
    public float arrowInterval;
    public int pierce;
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
        arrowInterval = stats[level - 1].projectileInterval;
        pierce = stats[level - 1].pierce;
    }

    protected override void Attack()
    {
        Vector3 spawnPoints = player.transform.position + new Vector3(0, Random.Range(-0.25f, 0.25f), 0);
        Instantiate(prefab, spawnPoints, Quaternion.identity);
        AudioManager.Instance.PlaySFX("Arrow");
    }

    protected override IEnumerator AttackRoutine()
    {
        while (!GameStateManager.Instance.isGameOver)
        {
            for (int i = 0; i < amount; i++)
            {
                Attack();
                yield return new WaitForSeconds(arrowInterval);
            }
            yield return new WaitForSeconds(cooldown);
        }
    }
}
