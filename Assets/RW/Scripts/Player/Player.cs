using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    private PlayerController playerController;

    public List<WeaponController> allWeapon;
    public List<WeaponController> currentWeaponList;
    public WeaponController weaponDefault;
    public CharacterAnimationController animationController;

    [Header("UI")]
    private InventoryUI inventoryUI;
    public HealthBar healthBar;
    private ExpBar expBar;
    private LevelUpUI levelUpUI;
    private LevelUI playerLevelText;
    public GameObject floatingTextPrefabs;
    [Header("Stats")]
    public int playerLevel;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private float playerSpeed = 3f;
    [HideInInspector]
    public float currentExp = 0;
    [SerializeField]
    private float maxExp;
    [SerializeField]
    private float takeHitInterval;
    private int maxAmountWeapon = 6;
    [HideInInspector]
    public int amountWeaponChoseWhenLvUp = 3;
    private bool isTakeHitInterval;
    private float timeAfterTakeHit;

    private void Awake()
    {
        levelUpUI = FindObjectOfType<LevelUpUI>();
        levelUpUI.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySFX("ThemeMusic");
        playerController = FindObjectOfType<PlayerController>();
        animationController = GetComponent<CharacterAnimationController>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        expBar = FindObjectOfType<ExpBar>();
        playerLevelText = FindObjectOfType<LevelUI>();

        UpgradeWeapon(weaponDefault);
        currentHealth = maxHealth;
        playerLevel = 1;
        maxExp = 100;
        timeAfterTakeHit = 0;

        maxAmountWeapon = 6;
        amountWeaponChoseWhenLvUp = 3;

        isTakeHitInterval = false;

        playerLevelText.UpdateLeveTextlUI(playerLevel);
        healthBar.SetMaxHeath(maxHealth);
        healthBar.SetCurrentHeath(currentHealth);
        expBar.SetMaxEXP(maxExp);
        expBar.SetCurrentEXP(currentExp);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameStateManager.Instance.isGameOver)
        {
            Move();
            if (isTakeHitInterval)
            {
                timeAfterTakeHit += Time.deltaTime;
                if (timeAfterTakeHit > takeHitInterval)
                {
                    isTakeHitInterval = false;
                    timeAfterTakeHit = 0;
                }
            }
        }
    }

    private void Move()
    {        
        {
            transform.Translate(playerController.moveDir * playerSpeed * Time.deltaTime);
        }
    }
    public void LoseHP(int dmg)
    {
        if (!isTakeHitInterval)
        {
            
            if (currentHealth > dmg)
            {
                ShowFloatingText(dmg);
                animationController.TakeHitAnimation();
                currentHealth -= dmg;
                isTakeHitInterval = true;
                AudioManager.Instance.PlaySFX("PlayerTakeHit");
            }
            else
            {
                AudioManager.Instance.PlaySFX("GameOver");
                Death();
            }
            healthBar.SetCurrentHeath(currentHealth);
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

    public void GainHP(int heath)
    {
        if (currentHealth + heath > maxHealth)
        {
            currentHealth = maxHealth;
        } else
        {
            currentHealth += heath;
        }
        healthBar.SetCurrentHeath(currentHealth);
    }
    public void GainEXP(float exp)
    {
        if (currentExp + exp >= maxExp)
        {
            float remainingExp = currentExp + exp - maxExp;
            UpLevel();
            GainEXP(remainingExp);
        }
        else
        {
            currentExp += exp;
        }
        expBar.SetCurrentEXP(currentExp);
    }

    public void GainChest()
    {

    }
    public void UpLevel()
    {
        GameStateManager.Instance.StopGame();
        AudioManager.Instance.PlaySFX("LevelUp");
        playerLevel++;
        maxExp *= 1.5f;
        currentExp = 0;
        maxHealth += 50;

        playerLevelText.UpdateLeveTextlUI(playerLevel);
        healthBar.SetMaxHeath(maxHealth);
        expBar.SetMaxEXP(maxExp);
        levelUpUI.SetLevelUp(PosibleChoseWeapon());
        levelUpUI.gameObject.SetActive(true);
    }

    private void Death()
    {
        currentHealth = 0;
        animationController.DeathAnimation();
        GameStateManager.Instance.GameOver();
    }
        
    public void UpgradeWeapon(WeaponController weaponController)
    {
        bool isAvailable = false;
        foreach (WeaponController weapon in currentWeaponList)
        {
            if (weapon == weaponController)
            {
                
                weapon.Upgrade();
                isAvailable = true;
                break;
            }
        }
        if (!isAvailable)
        {
            WeaponController newWeapon = Instantiate(weaponController, Vector2.zero, Quaternion.identity);
            currentWeaponList.Add(newWeapon);
            inventoryUI.UpdateInventoryUI(newWeapon.weaponSprite);
        }
        AudioManager.Instance.PlaySFX("PowerUp");
    }    

    public List<WeaponController> PosibleChoseWeapon()
    {
        List<WeaponController> selectableWeapon = new List<WeaponController>();
        for (int i = 0; i < amountWeaponChoseWhenLvUp; i++)
        {
            bool selected = false;
            while (!selected)
            {
                int rand = Random.Range(0, allWeapon.Count);
                bool weaponAvailable = false;
                foreach (WeaponController weapon in selectableWeapon)
                {
                    if (allWeapon[rand].stats[0].projectileName == weapon.stats[0].projectileName)
                    {
                        weaponAvailable = true;
                        break;
                    }
                }
                if (!weaponAvailable)
                {
                    foreach (WeaponController selectedWeapon in currentWeaponList)
                    {
                        if (selectedWeapon.stats[0].projectileName == allWeapon[rand].stats[0].projectileName && selectedWeapon.level < selectedWeapon.stats[0].maxLevel)
                        {
                            selectableWeapon.Add(selectedWeapon);
                            selected = true;
                        }
                    }
                    if (!selected && currentWeaponList.Count < maxAmountWeapon)
                    {
                        selectableWeapon.Add(allWeapon[rand]);
                        selected = true;
                    }
                }                
            }
        }
        return selectableWeapon;
    }
}
