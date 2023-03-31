using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public List<Image> skillInvetory;
    
    public void UpdateInventoryUI(Sprite skillSprite)
    {
        for (int i = 0; i < skillInvetory.Count; i++)
        {
            if (skillInvetory[i].sprite == null)
            {
                skillInvetory[i].sprite = skillSprite;
                skillInvetory[i].gameObject.SetActive(true);
                break;
            }
        }
    }
}
