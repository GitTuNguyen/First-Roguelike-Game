using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MagicCircleController : WeaponController
{
    public float attackDuration;
    public float timeAttack;
    private GameObject magicCircle;
    private float defaultRadius;
    private float radiusOfDamage;
    public LayerMask layerMask;
    private Player player;
    protected override void Start()
    {
        Debug.Log("start of circlecontroller");
        player = FindObjectOfType<Player>();
        magicCircle = Instantiate(prefab, player.transform.position, Quaternion.identity, player.transform);
        defaultRadius = transform.GetComponent<CircleCollider2D>().radius;
        timeAttack = 0;
        base.Start();
    }


    protected override void SetStats(int level)
    {
        base.SetStats(level);
        attackDuration = stats[level - 1].attackDuration;
        scale = stats[level - 1].projectileScale;
        radiusOfDamage = defaultRadius * scale;
        magicCircle.transform.localScale = new Vector3(scale, scale, scale);
    }

    protected override void Attack()
    {
        /*
        timeAttack = 0;
        Collider2D[] objectsInsideArea;
        objectsInsideArea = Physics2D.OverlapCircleAll(magicCircle.transform.position, radiusOfDamage, layerMask);
        while (timeAttack < attackDuration)
        {
            Array.Clear(objectsInsideArea, 0, objectsInsideArea.Length);
            objectsInsideArea = Physics2D.OverlapCircleAll(magicCircle.transform.position, radiusOfDamage, layerMask);
            foreach (Collider2D collision in objectsInsideArea)
            {
                collision.GetComponent<Enemy>().LoseHP(dame);
            }
            timeAttack += Time.deltaTime;
        }*/
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
