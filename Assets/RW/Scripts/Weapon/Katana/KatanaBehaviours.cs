using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaBehaviours : WeaponBehaviour
{

    protected override void Start()
    {
        weaponController = FindObjectOfType<KatanaController>();
        base.Start();
        Destroy(gameObject, weaponController.timeToDestroy);
    }

}
