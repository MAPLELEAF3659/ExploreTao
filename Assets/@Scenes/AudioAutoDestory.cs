using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAutoDestory : MonoBehaviour
{
    float t;
    void Update()
    {
        t += Time.deltaTime;
        if (t > gameObject.GetComponent<AudioSource>().clip.length)
            Destroy(gameObject);
    }
}
