using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Monster : Enemy
{

    [SerializeField]
    private int monsterDame = 20;

    private Player player;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized;
        base.Update();
    }
        

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().LoseHP(monsterDame);
        }
        if (collision.collider.CompareTag("LootItem"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().LoseHP(monsterDame);
        }
    }

    protected override void Death()
    {
        GameStateManager.Instance.UpdateEnemyKilled();
        base.Death();
    }

    protected override void FlipSprite()
    {
        base.FlipSprite();
        if (transform.position.x > player.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
