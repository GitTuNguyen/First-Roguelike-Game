using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenLoot : MonoBehaviour
{
    private Player player;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUpArea"))
        {
            player.GainHP(health);
            AudioManager.Instance.PlaySFX("PickUpChicken");
            Destroy(gameObject);
        }
    }

}
