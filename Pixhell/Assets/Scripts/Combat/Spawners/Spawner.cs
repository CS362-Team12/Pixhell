using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnEnemy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(spawnEnemy, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        var target = GameObject.FindWithTag("Enemy");
        if (!target) {
            Instantiate(spawnEnemy, transform.position, transform.rotation);
        }
    }
}
