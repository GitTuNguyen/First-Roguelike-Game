using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    public int experiences;
    [SerializeField]
    private float speed;

    private Player player;
    //private bool isPickedUp;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUpArea"))
        {
            player.GainEXP(experiences);
            AudioManager.Instance.PlaySFX("PickUpGem");
            Destroy(gameObject);
        }
    }
}
