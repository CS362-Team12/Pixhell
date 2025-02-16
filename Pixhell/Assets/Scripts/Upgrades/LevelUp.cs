using UnityEngine;

public class LevelUp : MonoBehaviour
{   
    public int experience = 0;
    int[] levelCaps = {0, 3, 10, 15, 22, 30, 40, 55, 70, 90, 100};
    int level = 1;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // TODO, cap to end of array
        if (experience >= levelCaps[Mathf.Min(level, levelCaps.Length-1)]) {
            experience -= levelCaps[level];
            level++;
            Debug.Log("Level Up!");
        }
    }
}
