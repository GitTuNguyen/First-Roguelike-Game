using UnityEngine;

public class VampireAxeBehaviour : WeaponBehaviour
{
    private Player player;
    public Animator animator;
    //private PlayerController playerController;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        //transform.parent = player.transform;
        //playerController = FindObjectOfType<PlayerController>();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
    protected override void Start()
    {
        weaponController = FindObjectOfType<VampireAxeController>();
        base.Start();
        //dir = playerController.frontdDir;
        //Rotate(dir);
        if (weaponController.level < weaponController.maxLevel)
        {
            Destroy(gameObject, weaponController.timeToDestroy);
        } else 
        {
            animator.SetTrigger("MaxLevel");
        }
    }
    protected override void Move()
    {
        transform.RotateAround(player.transform.position, Vector3.forward, speed*Time.deltaTime);
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Projectiles"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.collider.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            collision.transform.GetComponent<Enemy>().LoseHP(dame);            
        }
    }
}
