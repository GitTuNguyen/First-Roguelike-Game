using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxEXP(float maxEXP)
    {
        slider.maxValue = maxEXP;
    }
    public void SetCurrentEXP(float currentEXP)
    {
        slider.value = currentEXP;
    }
}
