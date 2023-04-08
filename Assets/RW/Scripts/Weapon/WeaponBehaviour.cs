using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public Vector2 dir;
    public float speed;
    public float dame;
    public float pierce;
    public WeaponController weaponController;

    protected virtual void Start()
    {        
        speed = weaponController.speed;
        dame = weaponController.dame;
        pierce = weaponController.pierce;
    }
    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {

    }

    protected virtual void Rotate(Vector2 dir)
    {
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Projectiles"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<Enemy>().LoseHP(dame);
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            pierce--;            
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), false);
        }
    }

    
}
