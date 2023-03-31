using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI levelText;
        
    public void UpdateLeveTextlUI(int playerLevel)
    {
        levelText.text = $"Level {playerLevel}";
    }
}
