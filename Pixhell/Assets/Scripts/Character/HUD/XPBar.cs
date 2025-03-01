using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class XPBar : MonoBehaviour
{

    public Sprite xpChunkImg;
    public Image xpBar;
    public Sprite xpBarBorder;

    TextMeshProUGUI text;
    LevelUp character;
    GameObject[] bars;
    int barCount = 100;
    float barWidth;
    float spacer = 0f;
    float xpBarOutline = 8f;

    float prevxp;

    void Start() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GeneratexpBar();
    }

    void Update() {
        if (prevxp != character.GetExperience()) {
            Debug.Log("UPDATING xp");
            UpdatexpDisplay();
         }
        prevxp = character.GetExperience();
    }

    void UpdatexpDisplay() {
        GameObject characterObj = GameObject.FindWithTag("Player");
        character = characterObj.GetComponent<LevelUp>();
        float percent = character.GetExperience() / character.GetNextXPRequirement();
        float showing = percent * barCount;

        for (int i = barCount - 1; i >= 0; i--) {
            bars[i].gameObject.SetActive(i < showing);
        }

        text.text = character.GetExperience() + " / " + character.GetNextXPRequirement();
    }

    void GeneratexpBar() {
        xpBar = GetComponent<Image>();
        GameObject characterObj = GameObject.FindWithTag("Player");
        character = characterObj.GetComponent<LevelUp>();
        prevxp = character.GetExperience();

        bars = new GameObject[barCount];
        barWidth = (xpBar.rectTransform.rect.width - (2 * xpBarOutline) - (spacer * 2)) / barCount;
        for (int i = 0; i < barCount; i++) {
            bars[i] = new GameObject("xpChunk" + i);
            Image xpChunk = bars[i].AddComponent<Image>();
            xpChunk.sprite = xpChunkImg;
            bars[i].transform.SetParent(xpBar.transform, false);

            float width = xpBar.rectTransform.rect.width;
            RectTransform rectBar = bars[i].GetComponent<RectTransform>();
            rectBar.sizeDelta = new Vector2(barWidth - spacer, xpBar.rectTransform.rect.height - 5); // Match height
            rectBar.anchorMin = new Vector2(0, 0.5f);
            rectBar.anchorMax = new Vector2(0, 0.5f);
            rectBar.pivot = new Vector2(0, 0.5f); // Left-center pivot

            rectBar.anchoredPosition = new Vector2((i * (rectBar.sizeDelta.x + spacer)) + xpBarOutline, 0);
        }

        // Overlay border on top
        GameObject duplicatedSprite = new GameObject("DuplicatedBorder");
        Image duplicatedxpChunk = duplicatedSprite.AddComponent<Image>();
        duplicatedxpChunk.sprite = xpBarBorder;

        duplicatedSprite.transform.SetParent(transform, false);

        RectTransform rectDuplicated = duplicatedSprite.GetComponent<RectTransform>();
        rectDuplicated.sizeDelta = new Vector2(xpBar.rectTransform.rect.width, xpBar.rectTransform.rect.height); // Match height
        rectDuplicated.anchorMin = new Vector2(0, 0.5f);
        rectDuplicated.anchorMax = new Vector2(0, 0.5f);
        rectDuplicated.pivot = new Vector2(0, 0.5f); // Left-center pivot
        rectDuplicated.anchoredPosition = new Vector2(0, 0);

        // TextBox
        GameObject xpText = new GameObject("xpText");
        text = xpText.AddComponent<TextMeshProUGUI>();
        text.transform.SetParent(transform, false);
        

        RectTransform textRect = xpText.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(xpBar.rectTransform.rect.width, xpBar.rectTransform.rect.height); // Match height
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
        foreach (Transform child in xpBar.transform)
        {
            Destroy(child.gameObject);
        }
        xpBar.gameObject.SetActive(false);
        GameObject characterObj = GameObject.FindWithTag("Player");
        if (characterObj != null) {
            character = characterObj.GetComponent<LevelUp>();
            if (scene.name != "StartMenu" && scene.name != "SelectRun" && scene.name != "Limbo" && scene.name != "CharacterSelect") {
                GeneratexpBar();
                UpdatexpDisplay();
                xpBar.gameObject.SetActive(true);
            }
        }
    
    }

}