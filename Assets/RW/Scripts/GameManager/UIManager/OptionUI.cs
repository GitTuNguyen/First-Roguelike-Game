using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    //public SpriteRenderer spriteWeapon;
    public Image skillImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI detaildText;
    public SkillController optionSkill;
    public void SetData(SkillController skillController)
    {
        optionSkill = skillController;
        skillImage.sprite = optionSkill.sprite;
        nameText.text = optionSkill.skillName;
        int nextLevel = optionSkill.level + 1;
        levelText.text = "Level " + nextLevel.ToString();        
        detaildText.text = optionSkill.getDescriptionNextLevel();
    }
}
