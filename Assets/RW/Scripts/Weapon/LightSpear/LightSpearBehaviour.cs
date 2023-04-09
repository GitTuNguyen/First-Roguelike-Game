using UnityEngine;

public class LightSpearBehaviour : WeaponBehaviour
{
    private Player player;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>());
    }
    protected override void Start()
    {
        weaponController = FindObjectOfType<LightSpearController>();
        base.Start();
        dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Rotate(dir);
        Destroy(gameObject, weaponController.timeToDestroy);
    }

    protected override void Move()
    {
        if (!GameStateManager.Instance.isGameOver)
        {
            transform.Translate(speed * Time.deltaTime * dir, Space.World);
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
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (pierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
