using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameWin : MonoBehaviour
{

    TextMeshProUGUI winText;
    TextMeshProUGUI loadText;

    string[] circles = {"Limbo", "Lust", "Gluttony", "Greed", "Wrath", "Heresy", "Violence", "Fraud", "Treachery"};

    void Start()
    {
        Debug.Log("HIDING GAME WIN");
        winText = transform.Find("YouWinText").GetComponent<TextMeshProUGUI>();
        loadText = transform.Find("LoadText").GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    public void OnGameWin()
    {
        gameObject.SetActive(true);
        StartCoroutine(UpdateText());
    }

    IEnumerator UpdateText()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        winText.text = currentScene + " has been defeated!";
        loadText.text = "";
        yield return new WaitForSeconds(3);
        if (circles[GameManager.maxArena] == SceneManager.GetActiveScene().name)
        {
            winText.text = "<color=#FFFFFF>" + circles[GameManager.maxArena + 1] + "</color> has been unlocked!";
            GameManager.maxArena += 1;
            yield return new WaitForSeconds(3);
        }

        for (int i = 3; i > 0; i--)
        {
            loadText.text = "Returning to lobby in " + i;
            yield return new WaitForSeconds(1);
        }
        loadText.text = "Returning...";
        yield return new WaitForSeconds(1.5f);
        GameManager.SavePlayerData();
        SceneManager.LoadScene("Limbo");
        gameObject.SetActive(false);
    }

}