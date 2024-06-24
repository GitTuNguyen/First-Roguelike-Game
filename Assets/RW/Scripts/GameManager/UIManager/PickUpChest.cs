using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpChest : MonoBehaviour
{
    Player player;
    public Image weaponImage;
    public float timeToChangeWeaponImage;
    public float timeAfterChangeImage;
    public float timeOpenChest = 3f;
    public float timeAfterOpenChest;
    public bool isOpened;
    public GameObject continueButton;
    private AudioSource openChestMusic;
    private List<WeaponController> upgradeableWeaponList;
    [SerializeField]
    private WeaponController upgradeWeapon;
    void Start()
    {
        player = FindObjectOfType<Player>();
        timeAfterChangeImage = 0;
        timeAfterOpenChest = 0;
        isOpened = false;
    }

    private void Update()
    {
        if(openChestMusic != null && openChestMusic.name == "OpenChest")
        {
            if(openChestMusic.isPlaying && timeAfterOpenChest < timeOpenChest)
            {
                timeAfterChangeImage += Time.unscaledDeltaTime;
                timeAfterOpenChest += Time.unscaledDeltaTime;
                Debug.Log("timeAfterChageImage: " + timeAfterChangeImage);

                if (timeAfterChangeImage >= timeToChangeWeaponImage)
                {
                    int randTemp = Random.Range(0, upgradeableWeaponList.Count);
                    weaponImage.sprite = upgradeableWeaponList[randTemp].weaponSprite;
                    timeAfterChangeImage = 0;
                }
            }
            else
            {
                openChestMusic.Stop();
                if (upgradeWeapon == null && upgradeableWeaponList.Count > 0)
                {
                    int rand = Random.Range(0, upgradeableWeaponList.Count);
                    upgradeWeapon = upgradeableWeaponList[rand];
                    weaponImage.sprite = upgradeWeapon.weaponSprite;
                    continueButton.SetActive(true);
                }
            }
            
        }
    }

    public void OpenChest()
    {
        if (!isOpened)
        {
            Debug.Log("OpenChest");
            AudioManager.Instance.PlayMusic("OpenChest");
            upgradeableWeaponList = UpgradeableWeapon();
            if (openChestMusic == null)
            {
                foreach (var sound in AudioManager.Instance.musicSounds)
                {
                    if (sound.name == "OpenChest" && sound.audioSource.isPlaying)
                    {
                        openChestMusic = sound.audioSource;
                        break;
                    }
                }
            }
            weaponImage.gameObject.SetActive(true);
            isOpened = true;
        } else {
            Skip();
        }
    }

    private void Skip()
    {
        timeAfterOpenChest += timeOpenChest;
    }
    public void ContinueGame()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        if (upgradeWeapon != null)
        {
            GameStateManager.Instance.ResumeGame();
            player.UpgradeWeapon(upgradeWeapon);
            upgradeWeapon = null;
            upgradeableWeaponList.Clear();
            isOpened = false;
            timeAfterOpenChest = 0;
        }
        
    }
    private List<WeaponController> UpgradeableWeapon()
    {
        List<WeaponController> upgradeableWapon = new List<WeaponController>();
        foreach(var weapon in player.currentWeaponList)
        {
            if(weapon.level < weapon.maxLevel)
            {
                upgradeableWapon.Add(weapon);
            }
        }
        return upgradeableWapon;
    }

}
