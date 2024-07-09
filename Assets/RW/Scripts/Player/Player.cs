using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;
using System.Linq;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public CharacterStats characterStats;
    //public List<SkillController> allSkill;
    public List<WeaponController> allWeaponList;
    public List<WeaponController> currentWeaponList;
    public List<PassivesSkillController> allPassiveSkillList;
    public List<PassivesSkillController> currentPassiveSkillList;
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
    private float pickUpRadius;
    public float weaponScale;
    [SerializeField]
    private float dodgeRate;
    public float bonusAmountProjectile;
    [SerializeField]
    private float bonusHealing;
    [SerializeField]
    private float takeHitInterval;
    private int maxNumberOfWeapon = 6;
    private int maxNumberOfPassive = 6;
    [HideInInspector]
    public int amountWeaponSelectableWhenLvUp = 3;
    [SerializeField]
    private bool isTakeHitInterval;
    private float timeAfterTakeHit;
    private float remainingExp;
    private bool isLevelingUp = false;

    [Header("Passive Skill Bonus")]
    public float reduceCooldown;
    public float increaseDame;
    public float armor;
    public float amountProjectile;
    public float moveSpeed;
    public float projectileSpeed;
    public float bonusMaxHealth;
    public float bonusExperience;
    
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
        // var tempAllWeaponList = new List<SkillController>(allWeaponList);
        // allSkill.AddRange(tempAllWeaponList);
        // var tempAllPassiveList = new List<SkillController>(allPassiveSkillList);
        // allSkill.AddRange(tempAllPassiveList);
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
        float takeHitRate = Random.Range(0f, 100f);
        if (takeHitRate > dodgeRate)
        {
            if (!isTakeHitInterval && currentHealth != 0)
            {            
                dmg = dmg - (int)armor > 0 ? dmg - (int)armor : 0;
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

    public void SetPassiveBonus(PassivesSkillStats passiveStat)
    {
        reduceCooldown += passiveStat.reduceCooldown;
        increaseDame += passiveStat.increaseDame;
        armor += passiveStat.armor;
        amountProjectile += passiveStat.amountProjectile;
        moveSpeed += passiveStat.moveSpeed;
        projectileSpeed += passiveStat.projectileSpeed;
        bonusMaxHealth += passiveStat.maxHealth;
        bonusExperience += passiveStat.bonusExperience;

        playerSpeed += moveSpeed;
        maxHealth +=  maxHealth*bonusMaxHealth;
        RefreshWeapontController();
    }

    private void RefreshWeapontController()
    {
        foreach(var weapon in currentWeaponList)
        {
            weapon.SetStats(weapon.level);
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
        health += (int)bonusHealing;
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
    public void GainEXP(float exp, bool isRemainingExp = false)
    {
        if (!isRemainingExp)
        {
            exp += exp * bonusExperience;
        }
        if (isLevelingUp)
        {
            remainingExp += exp;
        }
        else if (currentExp + exp >= maxExp)
        {
            remainingExp = currentExp + exp - maxExp;
            isLevelingUp = true;
            UpLevel();
        }
        else
        {
            currentExp += exp;
            remainingExp = 0;
            UIManager.Instance.levelUpUI.gameObject.SetActive(false);
            GameStateManager.Instance.ResumeGame();
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
        
        healthBar.SetMaxHeath(maxHealth);
        
        currentExp = maxExp;
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
            newWeapon.SetStats(1);
            currentWeaponList.Add(newWeapon);
            UIManager.Instance.UpdateInventoryUI(newWeapon.sprite);
        }
        
    }
    public void UpgradePassiveSkill(PassivesSkillController passivesSkillController)
    {
        bool isAvailable = false;
        foreach (PassivesSkillController passive in currentPassiveSkillList)
        {
            if (passive == passivesSkillController)
            {                
                passive.Upgrade();
                isAvailable = true;
                break;
            }
        }
        if (!isAvailable)
        {
            PassivesSkillController newPassiveSkill = Instantiate(passivesSkillController, transform.position, Quaternion.identity);
            newPassiveSkill.SetStats(1);
            currentPassiveSkillList.Add(newPassiveSkill);
            UIManager.Instance.UpdateInventoryUI(newPassiveSkill.sprite, false);
        }             
                    
    }
    public void UpgradeSkill(SkillController skill)
    {
        if (skill.skillType == SkillType.Weapon)
        {
            WeaponController weapon =  skill as WeaponController;
            if (weapon != null)
            {
                UpgradeWeapon(weapon);
            }
        } else 
        {
            PassivesSkillController passive = skill as PassivesSkillController;
            if (passive != null)
            {
                UpgradePassiveSkill(passive);
            }
        }
        AudioManager.Instance.PlaySFX("PowerUp");
        UIManager.Instance.UpdateLeveTextlUI(playerLevel);
        UIManager.Instance.SetMaxEXP(maxExp);        
        currentExp = 0;
        UIManager.Instance.SetCurrentEXP(currentExp);
        isLevelingUp = false;
        if (remainingExp > 0)
        {            
            GainEXP(remainingExp, true);
        } 
        else {            
            UIManager.Instance.levelUpUI.gameObject.SetActive(false);
            GameStateManager.Instance.ResumeGame();
        }
        //GameStateManager.Instance.ResumeGame();
    }

    public List<SkillController> SelectableWeaponToUpgrade()
    {
        List<SkillController> selectableSkill = new List<SkillController>();
        List<SkillController> pickedSkillList = new List<SkillController>();
        //List<SkillController> tempAllSkillList = new List<SkillController>(currentPassiveSkillList);
        
        selectableSkill.AddRange(currentWeaponList);
        if (currentWeaponList.Count < maxNumberOfWeapon)
        {
            List<SkillController> tempAllWeaponList = new List<SkillController>(allWeaponList);
            foreach(var weapon in currentWeaponList)
            {
                tempAllWeaponList.Remove(tempAllWeaponList.FirstOrDefault(c => c.skillName == weapon.skillName));
            }
            selectableSkill.AddRange(tempAllWeaponList);        
        }

        selectableSkill.AddRange(currentPassiveSkillList);        
        if (currentPassiveSkillList.Count < maxNumberOfPassive)
        {
            List<SkillController> tempAllPassiveList = new List<SkillController>(allPassiveSkillList);
            foreach(var passive in currentPassiveSkillList)
            {
                tempAllPassiveList.Remove(tempAllPassiveList.FirstOrDefault(c => c.skillName == passive.skillName));
            }
            selectableSkill.AddRange(tempAllPassiveList);
        }

        int amountWeaponsMaxLv = 0;
        foreach (WeaponController weapon in currentWeaponList)
        {
            if (weapon.level == weapon.maxLevel)
            {
                amountWeaponsMaxLv++;
                selectableSkill.Remove(selectableSkill.FirstOrDefault(c => c.skillName == weapon.skillName));
            }
        }
        
        Debug.Log("amountWeaponsMaxLv " + amountWeaponsMaxLv);

        int amountPassiveSkillMaxLv = 0;        
        foreach (PassivesSkillController passive in currentPassiveSkillList)
        {
            if (passive.level == passive.maxLevel)
            {
                amountPassiveSkillMaxLv++;
                selectableSkill.Remove(selectableSkill.FirstOrDefault(c => c.skillName == passive.skillName));
            }
        }        
        Debug.Log("amountPassiveSkillMaxLv " + amountPassiveSkillMaxLv);

        if (selectableSkill.Count > 0)
        {
            int numberSelectable = Math.Min(amountWeaponSelectableWhenLvUp, selectableSkill.Count);
            for (int i = 0; i < numberSelectable; i++)
            {
                bool isSelected = false;
                while (!isSelected)
                {
                    int rand = Random.Range(0, selectableSkill.Count);
                    bool isAlreadyPicked = false;
                    foreach (SkillController skill in pickedSkillList)
                    {
                        if (selectableSkill[rand].skillName == skill.skillName)
                        {
                            isAlreadyPicked = true;
                            break;
                        }
                    }
                    if (isAlreadyPicked)
                    {
                        continue;
                    }
                    pickedSkillList.Add(selectableSkill[rand]);
                    isSelected = true;
                }
            }            
        }
        return pickedSkillList;
    }
    public void SetCharacterDefaultStats()
    {
        playerLevel = 1;
        maxHealth = characterStats.defaultMaxHealth + characterStats.bonusMaxHealth;
        currentHealth = maxHealth;
        maxExp = characterStats.defaultMaxExp;
        currentExp = 0;
        playerSpeed = characterStats.defaultSpeed + characterStats.bonusSpeed;
        pickUpRadius = characterStats.defaultPickUpRadius + characterStats.bonusPickUpRadius;
        weaponScale = characterStats.defaultWeaponSize + characterStats.bonusWeaponSize;
        dodgeRate = characterStats.defaultDodgeRate + characterStats.bonusDodgeRate;
        bonusAmountProjectile = characterStats.bonusAmountProjectile;
        bonusHealing = characterStats.bonusHealing;

        pickUpArea.transform.localScale = new Vector3(pickUpRadius, pickUpRadius, pickUpRadius);
    }
    private void UpdateStatsLevelUp()
    {
        float tempMaxHealth = maxHealth;
        maxHealth += characterStats.healthForLevelUpStep;
        currentHealth = (currentHealth / tempMaxHealth) * maxHealth;
        maxExp += characterStats.expForLevelUpStep;
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
