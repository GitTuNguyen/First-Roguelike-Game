using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator characterAnimator;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void IdleAnimation()
    {
        characterAnimator.SetBool("IsRun", false);
    }

    public void RunAnimation()
    {
        characterAnimator.SetBool("IsRun", true);
    }

    public void TakeHitAnimation()
    {
        characterAnimator.SetTrigger("TakeHit");
    }

    public void DeathAnimation()
    {
        characterAnimator.SetBool("IsDeath", true);
    }

    public void SetFlip(bool isFlip)
    {
        spriteRenderer.flipX = isFlip;
    }
}
