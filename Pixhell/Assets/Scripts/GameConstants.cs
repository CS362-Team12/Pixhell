using UnityEngine;
using TMPro;

public static class GameConstants
{   
    // Turn to false for user builds. Use this to run stuff in debug mode only
    public const bool DEBUG = true;
    
    // Upgrade Constants
    public const int COMMON = 0;
    public const int UNCOMMON = 1;
    public const int RARE = 2;
    public const int LEGENDARY = 3;
    public static readonly string[] RARITY_STRINGS = {"Common", "Uncommon", "Rare", "Legendary"};
    public static readonly Color[] RARITY_COLORS = {Color.blue, Color.green, Color.red, Color.yellow};

    // Enemy Constants
    public const int MOVING = 0;
    public const int ATTACKING = 1;
    public const int IDLING = 2;
    public const int CHARGING = 3;
}