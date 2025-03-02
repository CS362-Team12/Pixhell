using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public Slider slider;
    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
