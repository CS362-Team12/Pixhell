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

    // James
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

    // Joshua
    [UnityTest]
    public IEnumerator CheckIfArcherSpawns()
    {
        SceneManager.LoadScene("CharacterSelect", LoadSceneMode.Single);
        yield return null;
        Assert.AreEqual(GameObject.FindWithTag("Player"), null);
        yield return new WaitForSeconds(1);
        GameObject archerButton = GameObject.Find("CharacterButton1");
        archerButton.GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(1);
        Assert.AreNotEqual(GameObject.FindWithTag("Player"), null);
        Assert.AreEqual(GameObject.FindWithTag("Player").name, "ArcherVariant(Clone)");
    }

    // Brendan
    [UnityTest]
    public IEnumerator CheckIfMageSpawns()
    {
        SceneManager.LoadScene("CharacterSelect", LoadSceneMode.Single);
        yield return null;
        Assert.AreEqual(GameObject.FindWithTag("Player"), null);
        yield return new WaitForSeconds(1);
        GameObject archerButton = GameObject.Find("CharacterButton3");
        archerButton.GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(1);
        Assert.AreNotEqual(GameObject.FindWithTag("Player"), null);
        Assert.AreEqual(GameObject.FindWithTag("Player").name, "MageVariant(Clone)");
    }
}
