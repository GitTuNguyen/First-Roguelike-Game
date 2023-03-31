using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    private Vector2 dir;
    private int pierce;
    private float speed;
    private ArrowController arrowController;
    private Player player;
    private PlayerController playerController;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerController = FindObjectOfType<PlayerController>();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
    private void Start()
    {
        arrowController = FindObjectOfType<ArrowController>();
        dir = playerController.frontdDir.normalized;
        speed = arrowController.speed;
        pierce = arrowController.pierce;
        Rotate(dir);
        Destroy(gameObject, arrowController.timeToDestroy);
    }

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
        transform.Translate(speed * Time.deltaTime * dir, Space.World);
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
            collision.gameObject.GetComponent<Enemy>().LoseHP(arrowController.dame);
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            pierce--;
            if (pierce == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), false);            
        }
    }
}
