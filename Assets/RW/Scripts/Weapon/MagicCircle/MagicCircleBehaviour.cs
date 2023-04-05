using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MagicCircleBehaviour : WeaponBehaviour
{
    [SerializeField]
    private float attackDuration;
    [SerializeField]
    private float timeAttack;
    [SerializeField]
    private float radiusOfDamage;
    [SerializeField]
    private float defaultRadius;

    private bool isAttacking;
    public LayerMask layerMask;
    // Start is called before the first frame update
    protected override void Start()
    {
        weaponController = FindObjectOfType<MagicCircleController>();        
        defaultRadius = transform.GetComponent<CircleCollider2D>().radius;
        Physics2D.IgnoreLayerCollision(8, 10);
        isAttacking = true;

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        radiusOfDamage = defaultRadius * weaponController.scale;
        transform.localScale = new Vector3(weaponController.scale, weaponController.scale, weaponController.scale);
        if (isAttacking)
        {
            Attack();
        }
        base.Update();
    }
    private void Attack()
    {
        timeAttack = 0;
        attackDuration = weaponController.stats[weaponController.level - 1].attackDuration;
        Collider2D[] objectsInsideArea;
        objectsInsideArea = Physics2D.OverlapCircleAll(transform.position, radiusOfDamage, layerMask);
        while (timeAttack < attackDuration)
        {
            Array.Clear(objectsInsideArea, 0, objectsInsideArea.Length);
            objectsInsideArea = Physics2D.OverlapCircleAll(transform.position, radiusOfDamage, layerMask);
            foreach (Collider2D collision in objectsInsideArea)
            {
                collision.GetComponent<Enemy>().LoseHP(dame);
            }
            timeAttack += Time.unscaledDeltaTime;
        }
        isAttacking = false;
    }
}
