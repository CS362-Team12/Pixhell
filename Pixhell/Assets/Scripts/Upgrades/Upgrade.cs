using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameConstants;



public class Upgrade : MonoBehaviour
{
    public virtual string title { get; set; }
    public virtual string description { get; set; }
    public virtual int rarity { get; set; }

    public PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("PlayerControllerPlayer");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ApplyUpgrade() {
        Debug.Log("Upgrae not implemented");
    }
}
