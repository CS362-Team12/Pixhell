using System.Xml;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float TimeToDestroy = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Animation anim = GetComponent<Animation>();
        AnimationCurve curve;
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;

        Keyframe[] keys;
        keys = new Keyframe[3];
        keys[0] = new Keyframe(0.0f, 0.0f);
        keys[1] = new Keyframe(.15f, 1f / transform.parent.localScale.magnitude + transform.parent.localScale.magnitude / 64f);
        curve = new AnimationCurve(keys);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curve);

        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);

        Destroy(gameObject, TimeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.parent.localScale.normalized * .5f/transform.parent.localScale.magnitude;
    }
}
