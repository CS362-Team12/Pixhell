using System.Collections;
using UnityEngine;
using static GameConstants;

public class HealCircle : MonoBehaviour
{
    float healAmount = .15f;
    float healTicks = 6;
    float healTime = 1.5f;
    private Collider2D collider;

    public void StartHealing()
    {
        // Higher heal for higher arenas?
        Debug.Log("Start");
        collider = GetComponent<Collider2D>();
        StartCoroutine(HealCoroutine());
    }


    private IEnumerator HealCoroutine()
    {
        Debug.Log(healTime);
        yield return new WaitForSeconds(healTime); 

        Debug.Log("Healing");
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
        Collider2D[] overlappingColliders = new Collider2D[20];
        int count = Physics2D.OverlapCollider(collider, contactFilter, overlappingColliders);

        // Loop through the overlapping colliders
        for (int i = 0; i < count; i++)
        {
            GameObject obj = overlappingColliders[i].gameObject;
            Debug.Log(obj);
            // Check if the object has the desired tag
            if (obj.CompareTag("Enemy"))
            {
                // Heal Animations?
                Debug.Log("Object with tag found: " + obj.name);
                obj.GetComponent<Enemy>().HealPercentage(healAmount);
            }
        }

        healTicks --;
        if (healTicks > 0) {
            StartCoroutine(HealCoroutine());
        } else {
            Destroy(gameObject);
        }
    }
}
