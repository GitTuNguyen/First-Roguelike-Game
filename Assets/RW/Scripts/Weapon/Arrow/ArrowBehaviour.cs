using UnityEngine;

public class ArrowBehaviour : WeaponBehaviour
{
    private Player player;
    private PlayerController playerController;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerController = FindObjectOfType<PlayerController>();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
    protected override void Start()
    {
        weaponController = FindObjectOfType<ArrowController>();
        base.Start();
        dir = playerController.frontdDir;
        Rotate(dir);
        Destroy(gameObject, weaponController.timeToDestroy);
    }
    protected override void Move()
    {
        if (GameStateManager.Instance.isGameOver)
        {
            return;
        }
        transform.Translate(speed * Time.deltaTime * dir, Space.World);
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
