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

    // Joshua
    [UnityTest]
    public IEnumerator CheckIfDEBUGIsOff()
    {
        yield return new WaitForSeconds(.5f); 
        Assert.AreEqual(GameObject.Find("DebugObject(Clone)"), null);
    }
    
    // James
    [UnityTest]
    public IEnumerator TestRunSelectCreateButton()
    {
        yield return null;
        SceneManager.LoadScene("SelectRun", LoadSceneMode.Single);
        yield return null;
        GameObject createButton = GameObject.Find("CreateNewRunButton");
        GameObject[] objs = UnityEngine.Object.FindObjectsOfType<GameObject>();
        int count = 0;
        foreach (GameObject obj in objs) 
        { 
            if (obj.name == "RunButtonPrefab(Clone)")
            {
                count++; 
            }
            
        }
        createButton.GetComponent<Button>().onClick.Invoke();
        yield return null;
        objs = UnityEngine.Object.FindObjectsOfType<GameObject>();
        int newCount = 0;
        foreach (GameObject obj in objs) 
        { 
            if (obj.name == "RunButtonPrefab(Clone)")
            {
                newCount++;
                Transform deleteButtonTransform = obj.transform.Find("DeleteRunButton");
                Button deleteButton = deleteButtonTransform.GetComponent<Button>();
                deleteButton.onClick.Invoke();
            }
            
        }
        if (count >= 3) {
            Assert.AreEqual(newCount, count);
        }
        else {
            Assert.AreEqual(newCount - 1, count);
        }
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

    //Brendan 
    [UnityTest]
    public IEnumerator CheckIfFloorExists()
    {
        SceneManager.LoadScene("Lust", LoadSceneMode.Single);
        yield return null;
        Assert.NotNull(GameObject.Find("Grid"));
        yield return null;

    }

    // Chris
    [UnityTest]
    public IEnumerator CheckIfLimboLoads()
    {
        yield return LoadSceneAndCheck("Limbo");
    }

    // Chris
    [UnityTest]
    public IEnumerator CheckIfLustLoads()
    {
        yield return LoadSceneAndCheck("Lust");
    }

    private IEnumerator LoadSceneAndCheck(string sceneName)
    {
        // Load scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Check if the active scene is the one we loaded
        Assert.AreEqual(sceneName, SceneManager.GetActiveScene().name, $"Scene {sceneName} failed to load.");
    }
    
    // Max
    [UnityTest]
    public IEnumerator TestStartButtonTriggersSound()
    {
        yield return null;
        GameObject startButton = GameObject.Find("StartButton");
        Assert.IsNotNull(startButton, "StartButton not found");
        AudioSource[] audioSources = Object.FindObjectsOfType<AudioSource>();
        Assert.IsTrue(audioSources.Length > 0, "No AudioSources found in scene");
        startButton.GetComponent<Button>().onClick.Invoke();
        yield return null;
        bool soundPlayed = false;
        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying || source.time > 0)
            {
                soundPlayed = true;
                break;
            }
        }
        Assert.IsTrue(soundPlayed, "No sound played after Start button click");
    }

    // Max
    [UnityTest]
    public IEnumerator TestLimboScenePlaysBackgroundMusic()
    {
        SceneManager.LoadScene("Limbo", LoadSceneMode.Single);
        yield return new WaitForSeconds(1);
        AudioSource[] audioSources = Object.FindObjectsOfType<AudioSource>();
        bool anyPlaying = false;
        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying && source.loop)
            {
                anyPlaying = true;
            }
        }
        Assert.IsTrue(anyPlaying, "Background music should play in Limbo scene");
    }

    // Tanush
    [UnityTest]
    public IEnumerator PortalTakesYouToLust()
    {
        // Get to Select Run Screen
        yield return null;
        GameObject startButton = GameObject.Find("StartButton");
        yield return new WaitForSeconds(2);
        Assert.AreEqual("StartMenu", SceneManager.GetActiveScene().name);
        startButton.GetComponent<Button>().onClick.Invoke();
        yield return null;



        // Select run

        // Clear all previous save files
        GameObject[] objs = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objs)
        {
            if (obj.name == "RunButtonPrefab(Clone)")
            {
                Transform deleteButtonTransform = obj.transform.Find("DeleteRunButton");
                Button deleteButton = deleteButtonTransform.GetComponent<Button>();
                deleteButton.onClick.Invoke();
            }

        }
        // Create new run and click on it
        yield return new WaitForSeconds(1);
        GameObject createRun = GameObject.Find("CreateNewRunButton");
        createRun.GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(4);
        GameObject saveButton = GameObject.Find("RunButtonPrefab(Clone)");
        saveButton.GetComponent<Button>().onClick.Invoke();
        yield return null;

        // Select character
        SceneManager.LoadScene("CharacterSelect", LoadSceneMode.Single);
        yield return null;
        GameObject archerButton = GameObject.Find("CharacterButton1");
        archerButton.GetComponent<Button>().onClick.Invoke();
        yield return null;
        yield return new WaitForSeconds(1);

        //Teleport player to portal and check if in lust after
        GameObject player = GameObject.FindWithTag("Player");
        GameObject portal = GameObject.Find("LimboPortal");
        player.transform.position = new Vector3(1.89f, 0.76f, 0);
        yield return new WaitForSeconds(3);
        Assert.AreEqual("Lust", SceneManager.GetActiveScene().name);
    }

    // Tanush
    [UnityTest]
    public IEnumerator CheckIfWarriorSpawns()
    {
        SceneManager.LoadScene("CharacterSelect", LoadSceneMode.Single);
        yield return null;
        Assert.AreEqual(GameObject.FindWithTag("Player"), null);
        yield return new WaitForSeconds(1);
        GameObject archerButton = GameObject.Find("CharacterButton2");
        archerButton.GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(1);
        Assert.AreNotEqual(GameObject.FindWithTag("Player"), null);
        Assert.AreEqual(GameObject.FindWithTag("Player").name, "WarriorVariant(Clone)");
    }
}

