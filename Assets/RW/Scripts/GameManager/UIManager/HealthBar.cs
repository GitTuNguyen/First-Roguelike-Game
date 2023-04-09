using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHeath(float maxHeath)
    {
        slider.maxValue = maxHeath;
    }
    public void SetCurrentHeath(float currentHeath)
    {
        slider.value = currentHeath;
    }
}
