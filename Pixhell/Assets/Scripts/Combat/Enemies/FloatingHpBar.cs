using UnityEngine;
using UnityEngine.UI;

public class FloatingHpBar : MonoBehaviour
{
    
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
