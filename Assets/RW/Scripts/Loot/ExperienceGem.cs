using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    public int experiences;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().GainEXP(experiences);
            AudioManager.Instance.PlaySFX("PickUpGem");
            Destroy(gameObject);
        }
    }
}
