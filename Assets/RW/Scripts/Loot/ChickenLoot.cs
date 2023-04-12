using UnityEngine;

public class ChickenLoot : MonoBehaviour
{
    public int health;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().GainHP(health);
            AudioManager.Instance.PlaySFX("PickUpChicken");
            Destroy(gameObject);
        }
    }

}
