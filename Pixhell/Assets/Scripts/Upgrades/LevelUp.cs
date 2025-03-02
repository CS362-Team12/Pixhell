using UnityEngine;


public class LevelUp : MonoBehaviour
{   
    public int experience = 0;
    int[] levelCaps = {0, 3, 10, 15, 22, 30, 40, 55, 70, 90, 100};
    int level = 1;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip levelUpSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int nextXP = (int) GetNextXPRequirement();
        if (experience >= nextXP && !gameObject.GetComponent<PlayerController>().IsDead()) {
            experience -= nextXP;
            level++;

            AudioManager.Instance.PlaySoundEffect(levelUpSound, 0.4f);

            GameObject UpgradeController = GameObject.Find("EventSystem");
            UpgradeController.GetComponent<UpgradeController>().TriggerLevelUp();
        }
    }

    public float GetNextXPRequirement() {
        return levelCaps[Mathf.Min(level, levelCaps.Length-1)];
    }

    public float GetLevel() {
        return level;
    }

    public float GetExperience() {
        return experience;
    }
}
