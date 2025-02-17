using UnityEngine;


public class LevelUp : MonoBehaviour
{   
    public int experience = 0;
    int[] levelCaps = {0, 3, 10, 15, 22, 30, 40, 55, 70, 90, 100};
    int level = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (experience >= levelCaps[Mathf.Min(level, levelCaps.Length-1)]) {
            experience -= levelCaps[level];
            level++;

            GameObject upgradeManager = GameObject.FindWithTag("UpgradeManager");
            upgradeManager.GetComponent<UpgradeManager>().TriggerLevelUp();
        }
    }
}
