using UnityEngine;

public class Chest : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUpArea"))
        {
            if (player == null)
            {
                player = FindObjectOfType<Player>();
            }
            AudioManager.Instance.PlaySFX("PickUpChicken");
            player.PickUpChest();
            Destroy(gameObject);
        }
    }
}
