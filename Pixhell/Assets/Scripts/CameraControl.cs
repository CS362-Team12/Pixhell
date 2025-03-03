using UnityEngine;
using Unity.Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public string characterTag = "Player"; // Tag for the character
    private CinemachineCamera cinemachineCamera;

    void Start()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();

        GameObject character = GameObject.FindGameObjectWithTag("Player");
        if (character != null)
        {
            cinemachineCamera.Follow = character.transform;
            cinemachineCamera.LookAt = character.transform;
        }
        else
        {
            Debug.LogError("Character with tag '" + characterTag + "' not found.");
        }
    }
    private void Update()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();

        GameObject character = GameObject.FindGameObjectWithTag("Player");
        cinemachineCamera.Follow = character.transform;
        cinemachineCamera.LookAt = character.transform;
    }
}