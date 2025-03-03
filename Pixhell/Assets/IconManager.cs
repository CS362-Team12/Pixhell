using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IconManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Icons")]
    public Sprite archer_1;
    public Sprite archer_2;

    public Sprite warrior_1;
    public Sprite warrior_2;

    public Sprite mage_1;
    public Sprite mage_2;

    Image SpecialOneIcon;
    Image SpecialOneCD;

    Image SpecialTwoIcon;
    Image SpecialTwoCD;

    public string player_c;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject characterObj = GameObject.FindWithTag("Player");
        if (characterObj != null)
        {
            if (scene.name != "StartMenu" && scene.name != "SelectRun")
            {
                gameObject.SetActive(true);
            }
        } else
        {
            gameObject.SetActive(false);
        }
    }

    public void InsertIcon(string player_class)
    {
        SpecialOneIcon = GameObject.Find("SpecialOneIcon").GetComponent<Image>();
        SpecialOneCD = GameObject.Find("SpecialOneOnCooldown").GetComponent<Image>();

        SpecialTwoIcon = GameObject.Find("SpecialTwoIcon").GetComponent<Image>();
        SpecialTwoCD = GameObject.Find("SpecialTwoOnCooldown").GetComponent<Image>();

        Debug.Log(player_class);
        if (player_class == "Archer")
        {
            SpecialOneIcon.sprite = archer_1;
            SpecialOneCD.sprite = archer_1;

            SpecialTwoIcon.sprite = archer_2;
            SpecialTwoCD.sprite = archer_2;
        }
        else if (player_class == "Warrior")
        {
            SpecialOneIcon.sprite = warrior_1;
            SpecialOneCD.sprite = warrior_1;

            SpecialTwoIcon.sprite = warrior_2;
            SpecialTwoCD.sprite = warrior_2;
        }
        else
        {
            SpecialOneIcon.sprite = mage_1;
            SpecialOneCD.sprite = mage_1;

            SpecialTwoIcon.sprite = mage_2;
            SpecialTwoCD.sprite = mage_2;
        }
    }
}
