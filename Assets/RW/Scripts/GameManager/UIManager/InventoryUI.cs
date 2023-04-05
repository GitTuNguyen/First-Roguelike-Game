using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public List<Image> weaponInvetory;
    
    public void UpdateInventoryUI(Sprite skillSprite)
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

    public void ResetGame()
    {
        foreach(Image weaponImage in weaponInvetory)
        {
            if(weaponImage.sprite != null)
            {
                weaponImage.sprite = null;
            }
        }
    }
}
