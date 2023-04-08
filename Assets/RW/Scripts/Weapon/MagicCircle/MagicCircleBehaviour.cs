using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MagicCircleBehaviour : WeaponBehaviour
{    
    [SerializeField]
    private float timeAttack;
    [SerializeField]
    private float radiusOfDamage;
    [SerializeField]
    private float defaultRadius;

    public LayerMask layerMask;
    // Start is called before the first frame update
    protected override void Start()
    {
        weaponController = FindObjectOfType<MagicCircleController>();
        defaultRadius = transform.GetComponent<CircleCollider2D>().radius;
        Debug.Log("(start)weaponController.attackDuration = " + weaponController.attackDuration);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        radiusOfDamage = defaultRadius * weaponController.projectileScale;
        transform.localScale = new Vector3(weaponController.projectileScale, weaponController.projectileScale, weaponController.projectileScale);                
    }
    public void Attack()
    {
        timeAttack = 0;
        if (weaponController == null)
        {
            weaponController = FindObjectOfType<MagicCircleController>();
        }
        while (timeAttack < weaponController.attackDuration)
        {            
            Collider2D[] enemyInsideAttackArea = Physics2D.OverlapCircleAll(transform.position, radiusOfDamage, layerMask);
            foreach (Collider2D collision in enemyInsideAttackArea)
            {
                collision.GetComponent<Enemy>().LoseHP(dame);

            }
            timeAttack += Time.unscaledDeltaTime;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("circle ignore collision with enemy");
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
