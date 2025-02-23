using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using static GameConstants;
using TMPro;

public class UpgradeController : MonoBehaviour
{
    public GameObject upgradeMenuUI;
    bool isPaused = false;
    public Upgrade[][] upgrades;
    // luck is currently not implemented
    public float luck = 0f;
    string[][] textNames = {
        new[] {"Upgrade1Title", "Upgrade1Description", "Upgrade1Rarity"},
        new[] {"Upgrade2Title", "Upgrade2Description", "Upgrade2Rarity"},
        new[] {"Upgrade3Title", "Upgrade3Description", "Upgrade3Rarity"},
    };
    Upgrade[] chosenUpgrades = new Upgrade[3];
    float[] odds = {.45f, .35f, .15f, .05f};
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upgradeMenuUI.SetActive(false);
        setUpgrades();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setUpgrades() {
        upgrades = new Upgrade[4][];

        upgrades[COMMON] = new Upgrade[] { 
            new AttackSpeedUp_U("Attack Speed Up","Increases attacks speed by 10%.", COMMON, 0.1f) 
        };
        upgrades[UNCOMMON] = new Upgrade[] { 
            new AttackSpeedUp_U("Attack Speed Up+", "Increases attacks speed by 20%.", UNCOMMON, 0.2f)
        };
        upgrades[RARE] = new Upgrade[] { 
            new AttackSpeedUp_U("Attack Speed Up++", "Increases attacks speed by 30%.", RARE, 0.3f)
        };
        upgrades[LEGENDARY] = new Upgrade[] { 
            new AttackSpeedUp_U("Attack Speed Up+++", "Increases attacks speed by 50%.", LEGENDARY, 0.5f)
        };
    }

    public void TogglePause() {
        isPaused = !isPaused;
        upgradeMenuUI.SetActive(isPaused);  // Show/hide the pause menu
        Time.timeScale = isPaused ? 0f : 1f;  // Freeze gameplay time when paused
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().enabled = isPaused ? false: true;
        GameObject pauseManager = GameObject.Find("EventSystem");
        pauseManager.GetComponent<PauseController>().enabled = isPaused ? false: true;
    }

    public void TriggerLevelUp() {
        TogglePause();
        for (int i = 0; i < 3; i++) {
            var random = Random.Range(0.0f, 1.0f);
            var rarity = -1;

            if (random < odds[COMMON]) {
                rarity = COMMON;
            } else if (random < odds[COMMON] + odds[UNCOMMON]) {
                rarity = UNCOMMON;
            } else if (random < odds[COMMON] + odds[UNCOMMON] + odds[RARE]) {
                rarity = RARE;
            } else {
                rarity = LEGENDARY;
            }

            var random2 = Random.Range(0, upgrades[rarity].Length);
            chosenUpgrades[i] = upgrades[rarity][random2];

            GameObject title_text = GameObject.Find(textNames[i][0]);
            title_text.GetComponent<TextMeshProUGUI>().text = chosenUpgrades[i].Title;
            GameObject description_text = GameObject.Find(textNames[i][1]);
            description_text.GetComponent<TextMeshProUGUI>().text = chosenUpgrades[i].Description;
            GameObject rarity_text = GameObject.Find(textNames[i][2]);
            rarity_text.GetComponent<TextMeshProUGUI>().text = RARITY_STRINGS[rarity];
            rarity_text.GetComponent<TextMeshProUGUI>().color = RARITY_COLORS[rarity];
        }
    }

    public void ChooseUpgrade(int upgradeNumber) {
        chosenUpgrades[upgradeNumber].ApplyUpgrade();
        TogglePause();
        // TODO: Apply Invincibility
    }
}
