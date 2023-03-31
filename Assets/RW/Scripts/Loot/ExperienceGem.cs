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

    private void Update()
    {
        /*
        if (isPickedUp)
        {
            Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            transform.Translate(dir * speed * Time.deltaTime);
            if (transform.position == player.transform.position)
            {
                Debug.Log("gain exp");
                player.GainEXP(experiences);
                Destroy(gameObject);
            }
        }
        */
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
