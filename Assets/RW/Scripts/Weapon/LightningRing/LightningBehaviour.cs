using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBehaviour : MonoBehaviour
{
    private LightningController lightningController;
    private Collider2D[] enemyInsideArea;
    public LayerMask enemyLayerMask;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        lightningController = FindObjectOfType<LightningController>();
        enemyInsideArea = Physics2D.OverlapCircleAll(transform.position, lightningController.currentArea, enemyLayerMask);
        foreach (Collider2D enemy in enemyInsideArea)
        {
            enemy.GetComponent<Enemy>().LoseHP(lightningController.dame);
        }
        animator = transform.GetChild(0).GetComponent<Animator>();
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
    /*
    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            Destroy(gameObject);
        }
    }
    */
}
