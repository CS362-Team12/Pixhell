using UnityEngine;
using static GameConstants;

public class DebugTestingScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject debugObject;
    
    void Start()
    {
        if (DEBUG && debugObject) {
            Instantiate(debugObject, transform.position, Quaternion.identity);
            Debug.Log("Debug Mode is On!");
        }
    }

}
