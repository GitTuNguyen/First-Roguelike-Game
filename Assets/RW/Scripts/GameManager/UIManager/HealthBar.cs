using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHeath(int maxHeath)
    {
        slider.maxValue = maxHeath;
    }
    public void SetCurrentHeath(int currentHeath)
    {
        slider.value = currentHeath;
    }
}
