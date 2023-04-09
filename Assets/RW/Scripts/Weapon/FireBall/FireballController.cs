
public class FireballController : WeaponController
{
    private Player player;
    private EnemySpawner enemySpawner;
    protected override void Start()
    {
        player = FindObjectOfType<Player>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        base.Start();        
    }


    protected override void Attack()
    {
        projectileSpawnPosition = player.transform.position;
        if (enemySpawner.enemyList.Count > 0)
        {
            base.Attack();
        }
        AudioManager.Instance.PlaySFX("FireBall");
    }
        
}
