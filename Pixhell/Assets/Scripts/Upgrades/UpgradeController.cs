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
    string[] textNames = {"Upgrade1Text", "Upgrade2Text", "Upgrade3Text"};
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
            if (random < odds[COMMON]) {
                var random2 = Random.Range(0, upgrades[COMMON].Length);
                chosenUpgrades[i] = upgrades[COMMON][random2];
            } else if (random < odds[COMMON] + odds[UNCOMMON]) {
                var random2 = Random.Range(0, upgrades[UNCOMMON].Length);
                chosenUpgrades[i] = upgrades[UNCOMMON][random2];
            } else if (random < odds[COMMON] + odds[UNCOMMON] + odds[RARE]) {
                var random2 = Random.Range(0, upgrades[RARE].Length);
                chosenUpgrades[i] = upgrades[RARE][random2];
            } else {
                var random2 = Random.Range(0, upgrades[LEGENDARY].Length);
                chosenUpgrades[i] = upgrades[LEGENDARY][random2];
            }

            GameObject text = GameObject.Find(textNames[i]);
            text.GetComponent<TextMeshProUGUI>().text = chosenUpgrades[i].Title;
        }
    }

    public void ChooseUpgrade(int upgradeNumber) {
        chosenUpgrades[upgradeNumber].ApplyUpgrade();
        TogglePause();
        // TODO: Apply Invincibility
    }
}
