using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public Animator characterAnimator;
    public SpriteRenderer spriteRenderer;
    
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

    public void ResetGame()
    {
        characterAnimator.SetBool("IsDeath", false);
        characterAnimator.SetTrigger("Reset");

    }

    public void SetFlip(bool isFlip)
    {
        spriteRenderer.flipX = isFlip;
    }
}
