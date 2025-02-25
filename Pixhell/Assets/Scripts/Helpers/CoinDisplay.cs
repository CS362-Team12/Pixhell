using TMPro;  // Make sure you have this at the top for TextMeshPro
using UnityEngine;
public class YourClass : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = "Coins: " + GameManager.coins.ToString();
    }
}
