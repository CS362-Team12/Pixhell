using TMPro;  // Make sure you have this at the top for TextMeshPro
using UnityEngine;
public class CoinDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public bool showLabel = true;

    void Update()
    {
        if (showLabel) {
            text.text = "Coins: " + GameManager.coins.ToString();
        }
        else {
            text.text = GameManager.coins.ToString();
        }
        
    }
}
