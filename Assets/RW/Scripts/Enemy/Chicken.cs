using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class Chicken : Enemy
{
    [SerializeField]
    private float timeMove;
    [SerializeField]
    private float timeToChangeDir;
    // Start is called before the first frame update
    protected override void Start()
    {
        timeMove = 0;
        dir = Vector2.right;
        canTakeHit = true;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        timeMove += Time.deltaTime;
        if (timeMove >= timeToChangeDir)
        {
            dir = -dir;
            timeMove = 0;
        }
        base.Update();
    }


    protected override void Death()
    {
        AudioManager.Instance.PlaySFX("ChickenDeath");
        base.Death();
    }
    
    protected override void FlipSprite()
    {
        base.FlipSprite();
        if (dir.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
