using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public CharacterStats characterStats;
    public List<WeaponController> allWeapon;
    public List<WeaponController> currentWeaponList;
    public CharacterAnimationController animationController;
    public GameObject pickUpArea;
    private PlayerController playerController;
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
    private float currentHealth;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float playerSpeed;
    [HideInInspector]
    public float currentExp;
    [SerializeField]
    private float maxExp;
    [SerializeField]
    private float takeHitInterval;
    private int maxNumberOfWeapon = 6;
    [HideInInspector]
    public int amountWeaponSelectableWhenLvUp = 3;
    private bool isTakeHitInterval;
    private float timeAfterTakeHit;

    private void Awake()
    {
        levelUpUI = FindObjectOfType<LevelUpUI>();
        if (levelUpUI != null)
        {
            levelUpUI.gameObject.SetActive(false);
        }        
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic("ThemeMusic");
        playerController = FindObjectOfType<PlayerController>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        expBar = FindObjectOfType<ExpBar>();
        playerLevelText = FindObjectOfType<LevelUI>();

        SetCharacterDefaultStats();
        UpgradeWeapon(characterStats.defaultWeapon);

        timeAfterTakeHit = 0;
        maxNumberOfWeapon = 6;
        amountWeaponSelectableWhenLvUp = 3;
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
                ShowFloatingText(dmg, true);
                animationController.TakeHitAnimation();
                currentHealth -= dmg;
                isTakeHitInterval = true;
                AudioManager.Instance.PlaySFX("PlayerTakeHit");
            }
            else
            {
                Death();
            }
            healthBar.SetCurrentHeath(currentHealth);
        }        
    }

    private void ShowFloatingText(float number, bool isLoseHp)
    {
        if (floatingTextPrefabs)
        {
            var text = Instantiate(floatingTextPrefabs, transform.position, Quaternion.identity, transform);
            if (isLoseHp)
            {
                text.GetComponent<TextMeshPro>().text = $"-{number}";
                text.GetComponent<TextMeshPro>().color = Color.red;
            } else
            {
                text.GetComponent<TextMeshPro>().text = $"+{number}";
                text.GetComponent<TextMeshPro>().color = Color.green;
            }
            
        }
    }

    public void GainHP(int health)
    {
        ShowFloatingText(health, false);
        if (currentHealth + health > maxHealth)
        {
            currentHealth = maxHealth;
        } else
        {
            currentHealth += health;
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
        UpdateStatsLevelUp();

        playerLevelText.UpdateLeveTextlUI(playerLevel);
        healthBar.SetMaxHeath(maxHealth);
        expBar.SetMaxEXP(maxExp);
        levelUpUI.SetLevelUp(SelectableWeaponToUpgrade());
        levelUpUI.gameObject.SetActive(true);
    }

    

    private void Death()
    {
        currentHealth = 0;
        AudioManager.Instance.PlaySFX("GameOver");
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

    //L?i ch?n weapon khi max lv
    public List<WeaponController> SelectableWeaponToUpgrade()
    {
        List<WeaponController> selectableWeapon = new List<WeaponController>();
        /*List<WeaponController> unselectableWeapon = new List<WeaponController>();
        for (int i = 0; i < amountWeaponSelectableWhenLvUp; i++)
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
                    foreach (WeaponController weapon in currentWeaponList)
                    {
                        if (weapon.level == weapon.maxLevel)
                        {
                            unselectableWeapon.Add(weapon);
                        } else if (weapon.stats[0].projectileName == allWeapon[rand].stats[0].projectileName)
                        {
                            selectableWeapon.Add(weapon);
                            selected = true;
                        }
                    }
                    if (!selected && currentWeaponList.Count < maxAmountWeapon)
                    {
                        bool canSelect = true;
                        foreach (WeaponController weapon in unselectableWeapon)
                        {
                            if (weapon.stats[0].projectileName == allWeapon[rand].stats[0].projectileName)
                            {
                                canSelect = false;
                                break;
                            }
                        }
                        if (canSelect)
                        {
                            selectableWeapon.Add(allWeapon[rand]);
                        }
                        selected = true;
                    }
                }                
            }
        }*/
        int weaponsMaxLv = 0;
        foreach (WeaponController weapon in currentWeaponList)
        {
            if (weapon.level == weapon.maxLevel)
            {
                weaponsMaxLv++;
            }
        }
        Debug.Log("weaponsMaxLv " + weaponsMaxLv);
        int numberSelectable;
        if (maxNumberOfWeapon - weaponsMaxLv < amountWeaponSelectableWhenLvUp)
        {
            numberSelectable = maxNumberOfWeapon - weaponsMaxLv;
            if (numberSelectable > 0)
            {
                if (currentWeaponList.Count == maxNumberOfWeapon)
                {
                    foreach (WeaponController weapon in currentWeaponList)
                    {
                        if (weapon.level < weapon.maxLevel)
                        {
                            selectableWeapon.Add(weapon);
                        }
                    }
                    return selectableWeapon;
                }                
            }
            else
            {
                return null;
            }
        }
        else
        {
            numberSelectable = amountWeaponSelectableWhenLvUp;            
        }

        
        for (int i = 0; i < numberSelectable; i++)
        {
            bool isSelected = false;
            while (!isSelected)
            {
                int rand = Random.Range(0, allWeapon.Count);
                bool isAlreadyHave = false;
                foreach (WeaponController weapon in selectableWeapon)
                {
                    if (allWeapon[rand].projectileName == weapon.projectileName)
                    {
                        isAlreadyHave = true;
                        break;
                    }
                }
                if (isAlreadyHave)
                {
                    continue;
                }
                bool isWeaponMaxLv = false;
                foreach(WeaponController weapon in currentWeaponList)
                {
                    if (allWeapon[rand].projectileName == weapon.projectileName)
                    {
                        if (weapon.level < weapon.maxLevel)
                        {
                            selectableWeapon.Add(weapon);
                            isSelected = true;
                        }
                        else
                        {
                            isWeaponMaxLv = true;
                        }
                        break;
                    }                                        
                }
                if (!isSelected && !isWeaponMaxLv)
                {
                    selectableWeapon.Add(allWeapon[rand]);
                    isSelected = true;
                }
                
            }
        }
        return selectableWeapon;
    }
    public void SetCharacterDefaultStats()
    {
        playerLevel = 1;
        maxHealth = characterStats.defaultMaxHealth;
        currentHealth = maxHealth;
        maxExp = characterStats.defaultMaxExp;
        currentExp = 0;
        playerSpeed = characterStats.defaultSpeed;
        float pickUpRadius = characterStats.defaultpickUpRadius;
        pickUpArea.transform.localScale = new Vector3(pickUpRadius, pickUpRadius, pickUpRadius);
    }
    private void UpdateStatsLevelUp()
    {
        float tempMaxHealth = maxHealth;
        maxHealth *= characterStats.healthForLevelUpStep;
        currentHealth = (currentHealth / tempMaxHealth) * maxHealth;
        maxExp *= characterStats.expForLevelUpStep;
        currentExp = 0;
    }
    public void ResetGame()
    {
        transform.position = Vector3.zero;
        foreach (WeaponController weapon in currentWeaponList)
        {
            Destroy(weapon);
        }
        currentWeaponList.Clear();

        SetCharacterDefaultStats();

        inventoryUI.ResetGame();
        playerLevelText.UpdateLeveTextlUI(playerLevel);
        healthBar.SetMaxHeath(maxHealth);
        healthBar.SetCurrentHeath(currentHealth);
        expBar.SetMaxEXP(maxExp);
        expBar.SetCurrentEXP(0);

        UpgradeWeapon(characterStats.defaultWeapon);
        animationController.ResetGame();
    }
}
