using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [Header("UI Objects")]
    public PickUpChest pickUpChestUI;
    public LevelUpUI levelUpUI;
    public GameObject gameOverUI;
    public GameObject pauseBoard;
    [Header("UI Elements")]
    public TextMeshProUGUI amountEnemyKilled;
    public TextMeshProUGUI timerText;
    public Slider expSlider;
    public List<Image> weaponInvetory;

    public List<Image> passiveInvetory;
    public TextMeshProUGUI levelText;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Enemy killed
    public void UpdateAmountEnemyKilledText()
    {
        amountEnemyKilled.text = $" {GameStateManager.Instance.enemyKilled:000000}";
    }

    //Exp bar
    public void SetMaxEXP(float maxEXP)
    {
        expSlider.maxValue = maxEXP;
    }
    public void SetCurrentEXP(float currentEXP)
    {
        expSlider.value = currentEXP;
    }

    //Iventory
    public void UpdateInventoryUI(Sprite skillSprite, bool isWeapon = true)
    {
        if (isWeapon)
        {
            for (int i = 0; i < weaponInvetory.Count; i++)
            {
                if (weaponInvetory[i].sprite == null)
                {
                    weaponInvetory[i].sprite = skillSprite;
                    weaponInvetory[i].gameObject.SetActive(true);
                    break;
                }
            }
        }
        else 
        {
            for (int i = 0; i < passiveInvetory.Count; i++)
            {
                if (passiveInvetory[i].sprite == null)
                {
                    passiveInvetory[i].sprite = skillSprite;
                    passiveInvetory[i].gameObject.SetActive(true);
                    break;
                }
            }
        }
    }

    public void ResetInventory()
    {
        foreach (Image weaponImage in weaponInvetory)
        {
            if (weaponImage.sprite != null)
            {
                weaponImage.sprite = null;
                weaponImage.gameObject.SetActive(false);
            }
        }
        foreach (Image skillImage in passiveInvetory)
        {
            if (skillImage.sprite != null)
            {
                skillImage.sprite = null;
                skillImage.gameObject.SetActive(false);
            }
        }
    }

    //Level
    public void UpdateLeveTextlUI(int playerLevel)
    {
        levelText.text = $"Level {playerLevel}";
    }

    //Timer
    private IEnumerator UpdateTimer()
    {
        while (!GameStateManager.Instance.isGameOver)
        {
            timerText.text = $"{GameStateManager.Instance.timer / 60:00} : {GameStateManager.Instance.timer % 60:00}";
            yield return new WaitForSeconds(1f);
        }
    }
}
