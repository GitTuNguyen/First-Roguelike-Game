using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : WeaponBehaviour
{
    private Player player;
    private Enemy closestEnemy;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
    // Start is called before the first frame update
    protected override void Start()
    {        
        weaponController = FindObjectOfType<FireballController>();
        base.Start();
        closestEnemy = FindClosestEnemy();
        dir = closestEnemy.transform.position - player.transform.position;
        Rotate(dir);
        Destroy(gameObject, weaponController.timeToDestroy);
    }
    protected override void Move()
    {
        if (GameStateManager.Instance.isGameOver)
        {
            return;
        }
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }
    
    private Enemy FindClosestEnemy()
    {
        float distanceMin = Mathf.Infinity;
        Enemy[] enemyList = GameObject.FindObjectsOfType<Enemy>();
        Enemy closestEnemy = null;
        foreach (Enemy currentEnemy in enemyList)
        {
            float distance = Vector2.Distance(currentEnemy.transform.position, player.transform.position);
            if (distance < distanceMin)
            {
                distanceMin = distance;
                closestEnemy = currentEnemy;
            }
        }

        return closestEnemy;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (pierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
