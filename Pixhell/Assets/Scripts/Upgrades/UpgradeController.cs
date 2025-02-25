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
            new AttackSpeedUp_U("Attack Speed Up", "Increases attack speed by 10%.", COMMON, 0.1f),
            new AttackDamageUp_U("Attack Damage Up", "Increases attack damage by 10%.", COMMON, 0.1f),
            new MoveSpeedUp_U("Move Speed Up", "Increases move speed by 10%.", COMMON, 0.1f),
        };
        upgrades[UNCOMMON] = new Upgrade[] { 
            new AttackSpeedUp_U("Attack Speed Up+", "Increases attack speed by 20%.", UNCOMMON, 0.2f),
            new AttackDamageUp_U("Attack Damage Up+", "Increases attack damage by 20%.", UNCOMMON, 0.2f),
            new MoveSpeedUp_U("Move Speed Up+", "Increases move speed by 20%.", UNCOMMON, 0.2f),
        };
        upgrades[RARE] = new Upgrade[] { 
            new AttackSpeedUp_U("Attack Speed Up++", "Increases attack speed by 30%.", RARE, 0.3f),
            new AttackDamageUp_U("Attack Damage Up++", "Increases attack damage by 30%.", RARE, 0.3f),
            new MoveSpeedUp_U("Move Speed Up++", "Increases move speed by 30%.", RARE, 0.3f),
        };
        upgrades[LEGENDARY] = new Upgrade[] { 
            new AttackSpeedUp_U("Attack Speed Up+++", "Increases attack speed by 50%.", LEGENDARY, 0.5f),
            new AttackDamageUp_U("Attack Damage Up+++", "Increases attack damage by 50%.", LEGENDARY, 0.5f),
            new MoveSpeedUp_U("Move Speed Up+++", "Increases move speed by 50%.", LEGENDARY, 0.5f),
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

            var upgradeFound = false;
            var loopCount = 0;
            while(!upgradeFound && loopCount < 5000) {
                var random2 = Random.Range(0, upgrades[rarity].Length);
                var upgrade = upgrades[rarity][random2];
                if (upgrade.IsValid()) {
                    chosenUpgrades[i] = upgrade;
                    upgradeFound = true;
                    // Set the upgrade to temporarily selected, so it can't show up in this upgrade cycle
                    upgrade.SetTempSelected(true);
                }
                loopCount ++;
            }
            if (loopCount >= 5000) {
                // This should never happen
                Debug.Log("ERROR: No Upgrade Found");
                return;
            }

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
        var upgrade = chosenUpgrades[upgradeNumber];
        upgrade.ApplyUpgrade();
        upgrade.SetSelected(true);
        
        // Unselect temp select upgrades so they can show up again, if not selected or if reusable
        for (int i = 0; i < 3; i++) {
            chosenUpgrades[i].SetTempSelected(false);
        }
        

        TogglePause();
        // TODO: Apply Invincibility
    }
}
