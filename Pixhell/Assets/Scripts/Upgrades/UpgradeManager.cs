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
            new AttackSpeedUp_U("Attack Speed Up+", "Increases attacks speed by 20%.", COMMON, 0.2f)
        };
        upgrades[RARE] = new Upgrade[] { 
            new AttackSpeedUp_U("Attack Speed Up++", "Increases attacks speed by 30%.", COMMON, 0.3f)
        };
        upgrades[LEGENDARY] = new Upgrade[] { 
            new AttackSpeedUp_U("Attack Speed Up+++", "Increases attacks speed by 50%.", COMMON, 0.5f)
        };
    }

    public void TriggerLevelUp() {
        Debug.Log(upgrades.Length);
        Debug.Log(upgrades[COMMON].Length);
        Debug.Log(upgrades[COMMON][0].Title);

        //isPaused = !isPaused;
        //upgradeMenuUI.SetActive(isPaused);  // Show/hide the pause menu
        //Time.timeScale = isPaused ? 0f : 1f;  // Freeze gameplay time when paused
        //GameObject player = GameObject.FindWithTag("Player");
        //player.GetComponent<PlayerController>().enabled = isPaused ? false: true;
        
        
        // TODO: Disable Pause Menu
        //SceneButtonBehavior();
    }
}
