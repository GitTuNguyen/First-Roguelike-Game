using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaBehaviours : MonoBehaviour
{
    private KatanaController katanaController;
    
    void Start()
    {
        katanaController = FindObjectOfType<KatanaController>();
        Debug.Log(katanaController);
        Destroy(gameObject, katanaController.timeToDestroy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().LoseHP(katanaController.dame);
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
