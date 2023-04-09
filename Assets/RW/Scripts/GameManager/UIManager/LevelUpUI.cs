using System.Collections.Generic;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    private Player player;
    public List<OptionUI> optionOptionUIList;
    public WeaponController weaponSelected;    
    public void SetLevelUp(List<WeaponController> selectableList)
    {
        for (int i = 0; i < optionOptionUIList.Count; i++)
        {
            optionOptionUIList[i].gameObject.SetActive(true);
            if (i < selectableList.Count)
            {
                optionOptionUIList[i].SetWeapon(selectableList[i]);
            } else
            {
                optionOptionUIList[i].gameObject.SetActive(false);
            }            
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
            AudioManager.Instance.PlaySFX("PowerUp");
            GameStateManager.Instance.ResumeGame();
            player.UpgradeWeapon(weaponSelected);            
        }        
    }
}
