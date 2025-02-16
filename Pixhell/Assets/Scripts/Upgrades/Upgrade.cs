using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameConstants;



public class Upgrade
{
    protected string title = "DEFAULT TITLE";
    protected string description = "DEFAULT DESCRIPTION";
    protected int rarity = COMMON;

    public PlayerController player;

    public string Title
    {
        get { return title; }
    }

    public string Description
    {
        get { return description; }
    }

    public virtual void ApplyUpgrade() {
        Debug.Log("Upgrae not implemented");
    }
}
