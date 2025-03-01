using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class HealthBar : MonoBehaviour
{

    public Sprite healthChunkImg;
    public Image healthBar;
    public Sprite healthBarBorder;

    TextMeshProUGUI text;
    PlayerController character;
    GameObject[] bars;
    int barCount = 100;
    float barWidth;
    float spacer = 0f;
    float healthBarOutline = 8f;

    float prevHealth;

    void Start() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GenerateHealthBar();
    }

    void Update() {
        if (prevHealth != character.health) {
            Debug.Log("UPDATING HEALTH");
            UpdateHealthDisplay();
         }
        prevHealth = character.health;
    }

    void UpdateHealthDisplay() {
        GameObject characterObj = GameObject.Find("walk-with-weapon-1");
        character = characterObj.GetComponent<PlayerController>();
        float percent = character.health / character.max_health;
        float showing = percent * barCount;

        for (int i = barCount - 1; i >= 0; i--) {
            bars[i].gameObject.SetActive(i < showing);
        }

        text.text = character.health + " / " + character.max_health;
    }

    void GenerateHealthBar() {
        healthBar = GetComponent<Image>();
        GameObject characterObj = GameObject.Find("walk-with-weapon-1");
        character = characterObj.GetComponent<PlayerController>();
        prevHealth = character.health;

        bars = new GameObject[barCount];
        barWidth = (healthBar.rectTransform.rect.width - (2 * healthBarOutline) - (spacer * 2)) / barCount;
        for (int i = 0; i < barCount; i++) {
            bars[i] = new GameObject("HealthChunk" + i);
            Image healthChunk = bars[i].AddComponent<Image>();
            healthChunk.sprite = healthChunkImg;
            bars[i].transform.SetParent(healthBar.transform, false);


            float width = healthBar.rectTransform.rect.width;
            RectTransform rectBar = bars[i].GetComponent<RectTransform>();
            rectBar.sizeDelta = new Vector2(barWidth - spacer, healthBar.rectTransform.rect.height - 5); // Match height
            rectBar.anchorMin = new Vector2(0, 0.5f);
            rectBar.anchorMax = new Vector2(0, 0.5f);
            rectBar.pivot = new Vector2(0, 0.5f); // Left-center pivot

            rectBar.anchoredPosition = new Vector2((i * (rectBar.sizeDelta.x + spacer)) + healthBarOutline, 0);
        }

        // Overlay border on top
        GameObject duplicatedSprite = new GameObject("DuplicatedBorder");
        Image duplicatedHealthChunk = duplicatedSprite.AddComponent<Image>();
        duplicatedHealthChunk.sprite = healthBarBorder;

        duplicatedSprite.transform.SetParent(transform, false);

        RectTransform rectDuplicated = duplicatedSprite.GetComponent<RectTransform>();
        rectDuplicated.sizeDelta = new Vector2(healthBar.rectTransform.rect.width, healthBar.rectTransform.rect.height); // Match height
        rectDuplicated.anchorMin = new Vector2(0, 0.5f);
        rectDuplicated.anchorMax = new Vector2(0, 0.5f);
        rectDuplicated.pivot = new Vector2(0, 0.5f); // Left-center pivot
        rectDuplicated.anchoredPosition = new Vector2(0, 0);

        // TextBox
        GameObject healthText = new GameObject("healthText");
        text = healthText.AddComponent<TextMeshProUGUI>();
        text.transform.SetParent(transform, false);
        

        RectTransform textRect = healthText.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(healthBar.rectTransform.rect.width, healthBar.rectTransform.rect.height); // Match height
        textRect.anchorMin = new Vector2(0, 0.5f);
        textRect.anchorMax = new Vector2(0, 0.5f);
        textRect.pivot = new Vector2(0, 0.5f); // Left-center pivot
        textRect.anchoredPosition = new Vector2(0, 0);
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;
        text.outlineWidth = 0.2f;
        text.outlineColor = Color.black;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        foreach (Transform child in healthBar.transform)
        {
            Destroy(child.gameObject);
        }
        healthBar.gameObject.SetActive(false);
        GameObject characterObj = GameObject.Find("walk-with-weapon-1");
        if (characterObj != null) {
            character = characterObj.GetComponent<PlayerController>();
            if (scene.name != "StartMenu" && scene.name != "SelectRun" && scene.name != "CharacterSelect") {
                GenerateHealthBar();
                healthBar.gameObject.SetActive(true);
            }
        }
    
    }

}