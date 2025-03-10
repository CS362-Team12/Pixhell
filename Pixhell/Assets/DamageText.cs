using System.Xml;
using UnityEngine;

public class DamageText : MonoBehaviour
{

    public float TimeToDestroy = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, TimeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.parent.localScale.normalized * .5f/transform.parent.localScale.magnitude;
    }
}
