using UnityEngine;

public class ArrowController : WeaponController
{    
    private Player player;
    protected override void Start()
    {
        player = FindObjectOfType<Player>();
        base.Start();
    }
       

    protected override void Attack()
    {
        projectileSpawnPosition = player.transform.position + new Vector3(0, Random.Range(-0.25f, 0.25f), 0);
        base.Attack();
        AudioManager.Instance.PlaySFX("Arrow");
    }

}
