using UnityEngine;

public class MagicCircleController : WeaponController
{
    
    private GameObject magicCircle;
    private Player player;
    protected override void Start()
    {
        player = FindObjectOfType<Player>();
        magicCircle = Instantiate(prefab, player.transform.position, Quaternion.identity, player.transform);        
        base.Start();
    }

    protected override void Attack()
    {
        magicCircle.GetComponent<MagicCircleBehaviour>().Attack();
    }
    

}
