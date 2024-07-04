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
    protected SpriteRenderer spriteRenderer;

    [Header("Take Hit Flash Effect")]
    [SerializeField]
    protected Material originalMaterial;
    [SerializeField]
    protected Material flashMaterial;
    [SerializeField]
    protected float flashDuration;
    [SerializeField]
    protected Coroutine flashCoroutine;

    [Header("Enemy Stats")]
    [SerializeField]
    protected float health = 100;
    [SerializeField]
    protected float speed = 3;
    [SerializeField]
    protected float takeHitInterval;
    [SerializeField]
    protected float removeTimeDelay;
    
    //
    protected Vector2 dir;
    protected float timeAfterTakeHit;
    protected bool canTakeHit;
    protected bool isDeath;
    protected EnemySpawner enemySpawner;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        timeAfterTakeHit = 0;
        canTakeHit = true;
        isDeath = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        flashMaterial = new Material(flashMaterial);
    }

    // Update is called once per frame
    protected virtual void Update()
    {        
        Move();
        FlipSprite();
        if (!canTakeHit && !isDeath)
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
        if (!isDeath)
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }        
    }


    public void SetSpawner(EnemySpawner spawner)
    {
        enemySpawner = spawner;
    }

    

    public void LoseHP(float dmg)
    {
        if (canTakeHit && !isDeath)
        {
            AudioManager.Instance.PlaySFX("EnemyTakeHit");
            ShowFloatingText(dmg);
            FlashEffect();
            if (health > dmg)
            {                
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
        isDeath = true;
        enemySpawner.RemoveEnemyFromList(gameObject);
        Destroy(gameObject, removeTimeDelay);
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

    protected void FlashEffect()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        flashCoroutine = StartCoroutine(FlashCoroutine());
    }

    protected IEnumerator FlashCoroutine()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material = originalMaterial;
        flashCoroutine = null;
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
            var lootItem = Instantiate(possibleDrop[index].itemPrefabs, this.transform.position, Quaternion.identity);
            GameStateManager.Instance.lootItemList.Add(lootItem);
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectiles") && !isDeath)
        {
            if (other.transform.TryGetComponent<WeaponBehaviour>(out var weapon))
            {
                LoseHP(weapon.dame);
                weapon.OnAttackEnemy();
            }            
        }        
    }
}
