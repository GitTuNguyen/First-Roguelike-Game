using System.Collections;
using UnityEngine;

public class MagicCircleController : WeaponController
{
    
    private GameObject magicCircle;
    protected override void Start()
    {
        magicCircle = Instantiate(prefab, player.transform.position, Quaternion.identity, player.transform);        
        base.Start();
    }

    protected override void Attack()
    {
        magicCircle.GetComponent<MagicCircleBehaviour>().Attack();
    }

}
