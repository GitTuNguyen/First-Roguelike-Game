using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    private Vector3 dir;
    public float speed;
    private FireballController fireballController;
    private Player player;
    private Enemy closestEnemy;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
    // Start is called before the first frame update
    void Start()
    {        
        fireballController = FindObjectOfType<FireballController>();
        closestEnemy = FindClosestEnemy();
        dir = closestEnemy.transform.position - player.transform.position;
        speed = fireballController.speed;
        Rotate(dir);
        Destroy(gameObject, fireballController.timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (GameStateManager.Instance.isGameOver)
        {
            return;
        }
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }

    private void Rotate(Vector2 dir)
    {
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Projectiles") || collision.collider.CompareTag("Terrains"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().LoseHP(fireballController.dame);
            Destroy(gameObject);
        }
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
}
