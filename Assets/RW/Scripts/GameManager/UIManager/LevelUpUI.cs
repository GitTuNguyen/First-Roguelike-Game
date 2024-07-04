using System.Collections.Generic;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    private Player player;
    public List<OptionUI> optionOptionUIList;
    public SkillController skillSelected;    
    public void SetLevelUp(List<SkillController> selectableList)
    {
        if (selectableList.Count == 0) 
        {
            return;
        }

        for (int i = 0; i < optionOptionUIList.Count; i++)
        {
            optionOptionUIList[i].gameObject.SetActive(true);
            if (i < selectableList.Count)
            {
                optionOptionUIList[i].SetData(selectableList[i]);
            } else
            {
                optionOptionUIList[i].gameObject.SetActive(false);
            }            
        }
    }

    public void SetSkillSelect(OptionUI option)
    {
        skillSelected = option.optionSkill;
    }

    public void UpgradeSkill()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        } 
        if (skillSelected != null)
        {
            AudioManager.Instance.PlaySFX("PowerUp");
            GameStateManager.Instance.ResumeGame();
            player.UpgradeSkill(skillSelected);            
        }        
    }
}
