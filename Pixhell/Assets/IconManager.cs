using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IconManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Icons")]
    public Sprite archer_1;
    public Sprite archer_2;
    public Sprite archer_dash;

    public Sprite warrior_1;
    public Sprite warrior_2;
    public Sprite warrior_dash;

    public Sprite mage_1;
    public Sprite mage_2;
    public Sprite mage_dash;

    Image SpecialOneIcon;
    Image SpecialOneCD;

    Image SpecialTwoIcon;
    Image SpecialTwoCD;

    Image DashIcon;
    Image DashCD;

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

        DashIcon = GameObject.Find("DashIcon").GetComponent<Image>();
        DashCD = GameObject.Find("DashOnCooldown").GetComponent<Image>();

        Debug.Log(player_class);
        if (player_class == "Archer")
        {
            SpecialOneIcon.sprite = archer_1;
            SpecialOneCD.sprite = archer_1;

            SpecialTwoIcon.sprite = archer_2;
            SpecialTwoCD.sprite = archer_2;

            DashIcon.sprite = archer_dash;
            DashCD.sprite = archer_dash;
        }
        else if (player_class == "Warrior")
        {
            SpecialOneIcon.sprite = warrior_1;
            SpecialOneCD.sprite = warrior_1;

            SpecialTwoIcon.sprite = warrior_2;
            SpecialTwoCD.sprite = warrior_2;

            DashIcon.sprite = warrior_dash;
            DashCD.sprite = warrior_dash;
        }
        else
        {
            SpecialOneIcon.sprite = mage_1;
            SpecialOneCD.sprite = mage_1;

            SpecialTwoIcon.sprite = mage_2;
            SpecialTwoCD.sprite = mage_2;

            DashIcon.sprite = mage_dash;
            DashCD.sprite = mage_dash;
        }
    }
}
