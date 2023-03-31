using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    //public SpriteRenderer spriteWeapon;
    public Image weaponImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI detaildText;
    public WeaponController optionWeapon;
    public void SetOption(WeaponController weaponController)
    {
        optionWeapon = weaponController;
        weaponImage.sprite = optionWeapon.weaponSprite;
        //spriteWeapon.sprite = weaponController.weaponSprite;
        nameText.text = optionWeapon.stats[0].projectileName;
        int nextLevel = optionWeapon.level + 1;
        levelText.text = "Level " + nextLevel.ToString();        
        detaildText.text = optionWeapon.getDescriptionNextLevel();
    }
}
