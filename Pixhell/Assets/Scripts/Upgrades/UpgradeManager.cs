using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using static GameConstants;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradeMenuUI;
    bool isPaused = false;
    public Upgrade[][] upgrades;
    // luck is currently not implemented
    public float luck = 0f;
    float[] odds = {.45f, .35f, .15f, .05f};
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    public void TriggerLevelUp() {
        var random = Random.Range(0.0f, 1.0f);
        if (random < odds[COMMON]) {
            var random2 = Random.Range(0, upgrades[COMMON].Length);
            upgrades[COMMON][0].ApplyUpgrade();
        } else if (random < odds[COMMON] + odds[UNCOMMON]) {
            var random2 = Random.Range(0, upgrades[UNCOMMON].Length);
            upgrades[UNCOMMON][0].ApplyUpgrade();
        } else if (random < odds[COMMON] + odds[UNCOMMON] + odds[RARE]) {
            var random2 = Random.Range(0, upgrades[RARE].Length);
            upgrades[RARE][0].ApplyUpgrade();
        } else {
            var random2 = Random.Range(0, upgrades[LEGENDARY].Length);
            upgrades[LEGENDARY][0].ApplyUpgrade();
        }
        

        //isPaused = !isPaused;
        //upgradeMenuUI.SetActive(isPaused);  // Show/hide the pause menu
        //Time.timeScale = isPaused ? 0f : 1f;  // Freeze gameplay time when paused
        //GameObject player = GameObject.FindWithTag("Player");
        //player.GetComponent<PlayerController>().enabled = isPaused ? false: true;
        
        
        // TODO: Disable Pause Menu
        //SceneButtonBehavior();
    }
}
