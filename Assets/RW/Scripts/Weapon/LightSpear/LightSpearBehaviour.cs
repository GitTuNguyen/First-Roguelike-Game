using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpearBehaviour : MonoBehaviour
{
    private Vector2 dir;
    private float speed;
    private LightSpearController lightSpearController;
    private Player player;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>());
    }
    private void Start()
    {
        lightSpearController = FindObjectOfType<LightSpearController>();
        dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        speed = lightSpearController.speed;
        Rotate(dir);
        Destroy(gameObject, lightSpearController.timeToDestroy);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!GameStateManager.Instance.isGameOver)
        {
            transform.Translate(speed * Time.deltaTime * dir, Space.World);
        }
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
            collision.gameObject.GetComponent<Enemy>().LoseHP(lightSpearController.dame);
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RightVision") || collision.CompareTag("LeftVision"))
        {
            dir.x = - dir.x;
            Rotate(dir);
        }
        if (collision.CompareTag("AboveVision") || collision.CompareTag("BelowVision"))
        {
            dir.y = - dir.y;
            Rotate(dir);
        }
    }
}
