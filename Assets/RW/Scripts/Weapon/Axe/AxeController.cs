
public class AxeController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        projectileSpawnPosition = player.transform.position;
        base.Attack();
        AudioManager.Instance.PlaySFX("LightSpear");
    }
        
}
