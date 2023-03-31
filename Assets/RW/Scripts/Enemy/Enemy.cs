using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class LootItems
    {
        public string name;
        public GameObject itemPrefabs;
        public float dropRate;
    }

    public List<LootItems> itemsList;
    public GameObject floatingTextPrefabs;

    [Header("Enemy Stats")]
    [SerializeField]
    private float enemyHP = 100;
    [SerializeField]
    private float enemySpeed = 3;
    [SerializeField]
    private int enemyDMG = 20;
    [SerializeField]
    private float timeToAttack = 1;
    public float cooldownTakeHit;

    private float timeAfterTakeHit;
    private bool canTakeHit = true;
    private EnemySpawner enemySpawner;
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        timeAfterTakeHit = cooldownTakeHit;
    }

    // Update is called once per frame
    void Update()
    {        
        Move();
        FlipSprite();
        if (!canTakeHit)
        {
            timeAfterTakeHit -= Time.deltaTime;
            if (timeAfterTakeHit < 0)
            {
                timeAfterTakeHit = cooldownTakeHit;
                canTakeHit = true;
            }
        }
    }

    private void Move()
    {        
        transform.Translate(new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized * enemySpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().LoseHP(enemyDMG);
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
            timeToAttack -= Time.deltaTime;
            if (timeToAttack < 0)
            {
                collision.gameObject.GetComponent<Player>().LoseHP(enemyDMG);
                timeToAttack = 1;
            }            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            timeToAttack = 1;
        }
    }

    public void SetSpawner(EnemySpawner spawner)
    {
        enemySpawner = spawner;
    }

    private void Death()
    {
        enemySpawner.RemoveEnemyFromList(gameObject);
        Destroy(gameObject);
    }

    public void LoseHP(float dmg)
    {
        if (canTakeHit)
        {
            AudioManager.Instance.PlaySFX("EnemyTakeHit");
            if (enemyHP > dmg)
            {
                Debug.Log("enemy take dmg");
                ShowFloatingText(dmg);
                enemyHP -= dmg;
                canTakeHit = false;
            }
            else
            {
                DropExp();
                GameStateManager.Instance.UpdateEnemyKilled();
                Death();
            }
        }        
    }
    private void ShowFloatingText(float dame)
    {
        if (floatingTextPrefabs)
        {
            var dameText = Instantiate(floatingTextPrefabs, transform.position, Quaternion.identity, transform);
            dameText.GetComponent<TextMeshPro>().text = $"-{dame}";
        }
    }

    private void FlipSprite()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void DropExp()
    {        
        float rand = Random.Range(0f, 100f);
        List<LootItems> possibleDrop = new List<LootItems>();
        foreach (LootItems possibleDropItem in itemsList)
        {
            if (rand < possibleDropItem.dropRate)
            {
                possibleDrop.Add(possibleDropItem);
            }
        }
        if (possibleDrop.Count > 0)
        {
            int index = Random.Range(0, possibleDrop.Count);
            Instantiate(possibleDrop[index].itemPrefabs, this.transform.position, Quaternion.identity);
        }
    }
}
