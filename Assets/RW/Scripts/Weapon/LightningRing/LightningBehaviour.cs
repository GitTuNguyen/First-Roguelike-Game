using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBehaviour : WeaponBehaviour
{    
    public LayerMask enemyLayerMask;
    [SerializeField]
    private float baseArea;
    [SerializeField]
    private float currentArea;
    private Animator animator;
    private Collider2D[] enemyInsideArea;
    // Start is called before the first frame update
    protected override void Start()
    {
        weaponController = FindObjectOfType<LightningController>();
        base.Start();
        currentArea = baseArea * weaponController.scale;
        enemyInsideArea = Physics2D.OverlapCircleAll(transform.position, currentArea, enemyLayerMask);
        foreach (Collider2D collision in enemyInsideArea)
        {
            if (collision.GetComponent<Enemy>() != null)
                {
                    collision.GetComponent<Enemy>().LoseHP(weaponController.dame);
                } else if (collision.GetComponent<Chicken>() != null)
                {
                    collision.GetComponent<Chicken>().LoseHP(weaponController.dame);
                }
        }
        animator = transform.GetChild(0).GetComponent<Animator>();
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

}
