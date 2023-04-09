using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public CharacterStats characterStats;
    public List<WeaponController> allWeapon;
    public List<WeaponController> currentWeaponList;
    public CharacterAnimationController animationController;
    public GameObject pickUpArea;
    private PlayerController playerController;
    [Header("UI")]
    public HealthBar healthBar;
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
    [SerializeField]
    private bool isTakeHitInterval;
    private float timeAfterTakeHit;
    private float remainingExp;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic("ThemeMusic");
        playerController = FindObjectOfType<PlayerController>();

        SetCharacterDefaultStats();
        UpgradeWeapon(characterStats.defaultWeapon);

        timeAfterTakeHit = 0;
        maxNumberOfWeapon = 6;
        amountWeaponSelectableWhenLvUp = 3;
        remainingExp = 0;
        isTakeHitInterval = false;

        UIManager.Instance.UpdateLeveTextlUI(playerLevel);
        healthBar.SetMaxHeath(maxHealth);
        healthBar.SetCurrentHeath(currentHealth);
        UIManager.Instance.SetMaxEXP(maxExp);
        UIManager.Instance.SetCurrentEXP(currentExp);
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
                if (timeAfterTakeHit >= takeHitInterval)
                {
                    isTakeHitInterval = false;
                    timeAfterTakeHit = 0;
                }
            }
        }
    }

    

    private void Move()
    {
        transform.Translate(playerController.moveDir * playerSpeed * Time.deltaTime);
    }
    public void LoseHP(int dmg)
    {
        if (!isTakeHitInterval && currentHealth != 0)
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

    

    public void PickUpChest()
    {
        Array.Find(AudioManager.Instance.musicSounds, musicSound => musicSound.name == "ThemeMusic").audioSource.Stop();
        GameStateManager.Instance.StopGame();
        UIManager.Instance.pickUpChestUI.gameObject.SetActive(true);
    }
    public void GainHP(int health)
    {
        ShowFloatingText(health, false);
        if (currentHealth + health > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += health;
        }
        healthBar.SetCurrentHeath(currentHealth);
    }
    public void GainEXP(float exp)
    {
        if (currentExp + exp >= maxExp)
        {
            remainingExp = currentExp + exp - maxExp;
            currentExp = maxExp;
            UIManager.Instance.SetCurrentEXP(currentExp);
            UpLevel();
        }
        else
        {
            currentExp += exp;
            remainingExp = 0;
        }
        UIManager.Instance.SetCurrentEXP(currentExp);
    }
    public void UpLevel()
    {
        Debug.Log("level up " + playerLevel);
        GameStateManager.Instance.StopGame();
        AudioManager.Instance.PlaySFX("LevelUp");
        playerLevel++; 
        UpdateStatsLevelUp();

        UIManager.Instance.UpdateLeveTextlUI(playerLevel);
        healthBar.SetMaxHeath(maxHealth);
        UIManager.Instance.SetCurrentEXP(currentExp);
        UIManager.Instance.SetMaxEXP(maxExp);
        UIManager.Instance.levelUpUI.SetLevelUp(SelectableWeaponToUpgrade());
        UIManager.Instance.levelUpUI.gameObject.SetActive(true);
    }
    private void Death()
    {
        currentHealth = 0;
        AudioManager.Instance.PlayMusic("GameOver");
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
            WeaponController newWeapon = Instantiate(weaponController, transform.position, Quaternion.identity);
            currentWeaponList.Add(newWeapon);
            UIManager.Instance.UpdateInventoryUI(newWeapon.weaponSprite);
        }
        AudioManager.Instance.PlaySFX("PowerUp");
        UIManager.Instance.levelUpUI.gameObject.SetActive(false);              
        if (remainingExp > 0)
        {
            GainEXP(remainingExp);
        }            
    }    

    public List<WeaponController> SelectableWeaponToUpgrade()
    {
        List<WeaponController> selectableWeapon = new List<WeaponController>();
        
        int amountWeaponsMaxLv = 0;
        foreach (WeaponController weapon in currentWeaponList)
        {
            if (weapon.level == weapon.maxLevel)
            {
                amountWeaponsMaxLv++;
            }
        }
        Debug.Log("amountWeaponsMaxLv " + amountWeaponsMaxLv);
        int numberSelectable;
        if (maxNumberOfWeapon - amountWeaponsMaxLv < amountWeaponSelectableWhenLvUp)
        {
            numberSelectable = maxNumberOfWeapon - amountWeaponsMaxLv;
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
        maxHealth += characterStats.healthForLevelUpStep;
        currentHealth = (currentHealth / tempMaxHealth) * maxHealth;
        maxExp += characterStats.expForLevelUpStep;
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

        UIManager.Instance.ResetInventory();
        UIManager.Instance.UpdateLeveTextlUI(playerLevel);
        healthBar.SetMaxHeath(maxHealth);
        healthBar.SetCurrentHeath(currentHealth);
        UIManager.Instance.SetMaxEXP(maxExp);
        UIManager.Instance.SetCurrentEXP(0);

        UpgradeWeapon(characterStats.defaultWeapon);
        animationController.ResetGame();
    }
}
