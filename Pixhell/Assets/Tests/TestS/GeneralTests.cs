using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.TestTools;
using System.Collections;

public class GeneralTests : MonoBehaviour
{

    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("Global", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

    [UnityTest]
    public IEnumerator TestButtonClickTakesYouToSelectRunScene()
    {
        yield return null;
        GameObject startButton = GameObject.Find("StartButton");
        yield return new WaitForSeconds(2);
        Assert.AreEqual("StartMenu", SceneManager.GetActiveScene().name);
        startButton.GetComponent<Button>().onClick.Invoke();
        yield return null;
        Assert.AreEqual("SelectRun", SceneManager.GetActiveScene().name);
    }

    [UnityTest]
    public IEnumerator CheckIfPauseMenuPausesOnStartMenu()
    {
        yield return null;
        Input.simulateKeyPress = KeyCode.Escape;
        Assert.AreEqual(GameObject.Find("EventSystem").GetComponent<PauseController>().isPaused, false);
    }
}
