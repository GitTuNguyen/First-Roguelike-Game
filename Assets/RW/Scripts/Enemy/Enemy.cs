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
    public SpriteRenderer spriteRenderer;

    [Header("Enemy Stats")]
    [SerializeField]
    protected float health = 100;
    [SerializeField]
    protected float speed = 3;
    [SerializeField]
    protected float takeHitInterval;

    protected Vector2 dir;
    protected float timeAfterTakeHit;
    protected bool canTakeHit = true;
    protected EnemySpawner enemySpawner;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        timeAfterTakeHit = 0;
    }

    // Update is called once per frame
    protected virtual void Update()
    {        
        Move();
        FlipSprite();
        if (!canTakeHit)
        {
            timeAfterTakeHit += Time.deltaTime;
            if (timeAfterTakeHit >= takeHitInterval)
            {
                timeAfterTakeHit = 0;
                canTakeHit = true;
            }
        }
    }

    protected virtual void Move()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }


    public void SetSpawner(EnemySpawner spawner)
    {
        enemySpawner = spawner;
    }

    

    public void LoseHP(float dmg)
    {
        if (canTakeHit)
        {
            AudioManager.Instance.PlaySFX("EnemyTakeHit");
            if (health > dmg)
            {
                ShowFloatingText(dmg);
                health -= dmg;
                canTakeHit = false;
            }
            else
            {
                Death();
            }
        }        
    }
    protected virtual void Death()
    {
        DropItem();
        enemySpawner.RemoveEnemyFromList(gameObject);
        Destroy(gameObject);
    }
    protected void ShowFloatingText(float dame)
    {
        if (floatingTextPrefabs)
        {
            var dameText = Instantiate(floatingTextPrefabs, transform.position, Quaternion.identity, transform);
            dameText.GetComponent<TextMeshPro>().text = $"-{dame}";
            dameText.GetComponent<TextMeshPro>().color = Color.white;
        }
    }

    protected virtual void FlipSprite()
    {
        
    }

    protected void DropItem()
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
