using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    AudioSource bgm;

    [Range(0,1)]
    public float fadeSpeed=0.1f;
    [Range(0, 1)]
    public float maxFadeInVolume= 0.5f;
    [Range(0, 1)]
    public float fadeInPerFrame = 0.1f;
    [Range(0, 1)]
    public float fadeOutPerFrame = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        bgm = GetComponent<AudioSource>();
    }

    public void FadeChangeBGM(AudioClip clip)
    {
        StartCoroutine(FadeInAudio(clip));
    }

    public void ChangeBGM(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.Play();
    }

    IEnumerator FadeInAudio(AudioClip bgmClip)
    {
        yield return new WaitForFixedUpdate();
        while (bgm.volume < maxFadeInVolume)
        {
            bgm.volume -= fadeOutPerFrame;
            yield return new WaitForSeconds(fadeSpeed);
        }
        bgm.volume = 0f;
        bgm.clip = bgmClip; 
        bgm.Play();
        while (bgm.volume <maxFadeInVolume)
        {
            bgm.volume += fadeInPerFrame;
            yield return new WaitForSeconds(fadeSpeed);
        }
        bgm.volume = maxFadeInVolume;
    }
}
