using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IconManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Icons")]
    public Sprite archer_class;
    public Sprite warrior_class;
    public Sprite mage_class;

    public Image SpecialOne;
    public Image SpecialTwo;

    void Start()
    {
        Debug.Log("Generateddddddddddddddddddddddddd");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameObject.SetActive(false);
        GameObject characterObj = GameObject.FindWithTag("Player");
        if (characterObj != null)
        {
            if (scene.name != "StartMenu" && scene.name != "SelectRun")
            {
                gameObject.SetActive(true);
                InsertIcon();
            }
        }
    }

    void InsertIcon()
    {
        SpecialOne = GameObject.Find("SpecialOneOnCooldown").GetComponent<Image>();
        SpecialTwo = GameObject.Find("SpecialTwoOnCooldown").GetComponent<Image>();

        GameObject characterObj = GameObject.FindWithTag("Player");
        ArcherClass archerClass = characterObj.GetComponent<ArcherClass>();
        Debug.Log(archerClass);
        Debug.Log("RANNNNNNNNNNNNNNNNNNNNNNNNNNNNN");
    }
}
