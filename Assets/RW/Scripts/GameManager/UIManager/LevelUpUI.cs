using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    private Player player;
    public List<OptionUI> optionOptionUIList;
    public WeaponController weaponSelected;    

    public void SetLevelUp(List<WeaponController> selectableList)
    {
        for (int i = 0; i < selectableList.Count; i++)
        {
            optionOptionUIList[i].SetOption(selectableList[i]);
        }
    }

    public void SetWeaponSelect(OptionUI option)
    {
        weaponSelected = option.optionWeapon;
    }

    public void UpgradeWeapon()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        } 
        if (weaponSelected != null)
        {
            AudioManager.Instance.PlayPowerUpSFX();
            player.UpgradeWeapon(weaponSelected);
            GameStateManager.Instance.RemuseGame();
            this.gameObject.SetActive(false);
        }        
    }
}
