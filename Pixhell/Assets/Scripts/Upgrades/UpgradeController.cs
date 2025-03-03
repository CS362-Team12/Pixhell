using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using static GameConstants;
using TMPro;

public class UpgradeController : MonoBehaviour
{
    public GameObject upgradeMenuUI;
    GameObject player;
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

    // Array coordinates for upgrade to force being selected
    // Set to -1 to turn off, attched to DEBUG to be off for non DEBUG builds
    int[] forcedUpgrade = {-1, 3};

    [Header("Audio Settings")]
    [SerializeField] private AudioClip selectSound;

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: Reseting Upgrades");
        upgradeMenuUI.SetActive(false);
        setUpgrades();
        player = GameObject.FindWithTag("Player");
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
            new DashRangeUp_U("Dash Range Up", "Increases dash distance by 30%.", COMMON, 0.3f),
            new MaxHealthUp_U("Max Health Up", "Increases max health by 20%.", COMMON, 0.2f),
        };
        upgrades[UNCOMMON] = new Upgrade[] { 
            new AttackSpeedUp_U("Attack Speed Up+", "Increases attack speed by 20%.", UNCOMMON, 0.2f),
            new AttackDamageUp_U("Attack Damage Up+", "Increases attack damage by 20%.", UNCOMMON, 0.2f),
            new MoveSpeedUp_U("Move Speed Up+", "Increases move speed by 20%.", UNCOMMON, 0.2f),
            new MaxHealthUp_U("Max Health Up+", "Increases max health by 30%.", UNCOMMON, 0.3f),
        };
        upgrades[RARE] = new Upgrade[] { 
            new AttackSpeedUp_U("Attack Speed Up++", "Increases attack speed by 30%.", RARE, 0.3f),
            new AttackDamageUp_U("Attack Damage Up++", "Increases attack damage by 30%.", RARE, 0.3f),
            new MoveSpeedUp_U("Move Speed Up++", "Increases move speed by 30%.", RARE, 0.3f),
            new MaxHealthUp_U("Max Health Up++", "Increases max health by 40%.", RARE, 0.4f),
        };
        upgrades[LEGENDARY] = new Upgrade[] { 
            new AttackSpeedUp_U("Attack Speed Up+++", "Increases attack speed by 50%.", LEGENDARY, 0.5f),
            new AttackDamageUp_U("Attack Damage Up+++", "Increases attack damage by 50%.", LEGENDARY, 0.5f),
            new MoveSpeedUp_U("Move Speed Up+++", "Increases move speed by 50%.", LEGENDARY, 0.5f),
            new DashRangeUp_U("Dash Range Up+", "Doubles dash distance.", LEGENDARY, 1f),
            new MaxHealthUp_U("Max Health Up+++", "Increases max health by 60%.", LEGENDARY, 0.6f),
        };
    }

    public void TogglePause() {
        isPaused = !isPaused;
        upgradeMenuUI.SetActive(isPaused);  // Show/hide the pause menu
        Time.timeScale = isPaused ? 0f : 1f;  // Freeze gameplay time when paused
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
            
            // FORCED UPGRADE DEBUG OPTION
            Debug.Log(DEBUG);
            if (DEBUG && forcedUpgrade[0] != -1 && upgrades[forcedUpgrade[0]][forcedUpgrade[1]].IsValid()) {
                Debug.Log("Forced Upgrade Active");
                chosenUpgrades[i] = upgrades[forcedUpgrade[0]][forcedUpgrade[1]];
                upgradeFound = true;
                upgrades[forcedUpgrade[0]][forcedUpgrade[1]].SetTempSelected(true);
            }

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
            rarity_text.GetComponent<TextMeshProUGUI>().text = RARITY_STRINGS[chosenUpgrades[i].Rarity];
            rarity_text.GetComponent<TextMeshProUGUI>().color = RARITY_COLORS[chosenUpgrades[i].Rarity];
        }
    }

    public void ChooseUpgrade(int upgradeNumber) {
        var upgrade = chosenUpgrades[upgradeNumber];
        upgrade.ApplyUpgrade();
        upgrade.SetSelected(true);

        AudioManager.Instance.PlaySoundEffect(selectSound, 0.3f);

        // Unselect temp select upgrades so they can show up again, if not selected or if reusable
        for (int i = 0; i < 3; i++) {
            chosenUpgrades[i].SetTempSelected(false);
        }
        
        TogglePause();
        player.GetComponent<PlayerController>().StartImmune();
    }
}
