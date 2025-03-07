using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameConstants;
using System.Linq;



public class Upgrade
{
    protected string title = "DEFAULT TITLE";
    protected string description = "DEFAULT DESCRIPTION";
    protected int rarity = COMMON;
    // Able to be applied to the player multiple times
    protected bool reusable = true;
    // Selected and applied at least once to the player
    bool selected = false;
    // Selected in the current upgrade cycle
    bool tempSelected = false;
    // All characters are valid by default
    protected string[] validCharacters = {"Archer", "Warrior", "Mage"};
    

    public PlayerController player;

    public string Title
    {
        get { return title; }
    }

    public string Description
    {
        get { return description; }
    }

    public int Rarity
    {
        get { return rarity; }
    }

    public void SetSelected(bool s) {
        selected = s;
    }

    public void SetTempSelected(bool ts) {
        tempSelected = ts;
    }

    public bool IsValid() {
        var character = GameObject.Find("EventSystem").GetComponent<CharacterSelectController>().character;
        
        // Current player character isn't in the allowed character set
        if (!validCharacters.Contains(character)) {
            return false;
        }

        // Already picked in this upgrade cycle, can't be picked
        if (tempSelected) {
            return false;
        }

        // Not reusable? If it's bene selected, it can't be picked again
        if (!reusable) {
            return !selected;
        }

        // It's valid!
        return true;
    }

    public virtual void ApplyUpgrade() {
        Debug.Log("Upgrae not implemented");
    }


}
