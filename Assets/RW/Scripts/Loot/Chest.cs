using UnityEngine;

public class Chest : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX("PickUpChicken");
            collision.gameObject.GetComponent<Player>().PickUpChest();
            Destroy(gameObject);
        }
    }
}
