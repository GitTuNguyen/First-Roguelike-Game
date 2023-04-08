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

    //private bool isPickedUp;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUpArea"))
        {
            FindObjectOfType<Player>().GainEXP(experiences);
            AudioManager.Instance.PlaySFX("PickUpGem");
            Destroy(gameObject);
        }
    }
}
