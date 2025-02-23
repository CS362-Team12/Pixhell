using UnityEngine;
using TMPro;

public static class GameConstants
{
    public const int COMMON = 0;
    public const int UNCOMMON = 1;
    public const int RARE = 2;
    public const int LEGENDARY = 3;
    public static readonly string[] RARITY_STRINGS = {"Common", "Uncommon", "Rare", "Legendary"};
    public static readonly Color[] RARITY_COLORS = {Color.blue, Color.green, Color.red, Color.yellow};
}