using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingBtnController : MonoBehaviour
{
    public Animator settingAni;
    public Text settingTitle, volumeTitle, languageTitle, languageName;
    public AudioSource bgm;
    public Slider volumeSlider;
    public Font pixelFont, genFont;
    public float volume;

    public void Start()
    {
        bgm = GameObject.FindGameObjectWithTag("BGMManager").GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("volume");
        bgm.volume = volume;
        volumeSlider.value = volume;
    }

    public void Toggle()
    {
        settingAni.SetTrigger("toggle");
    }

    public void OnLanguageChanged()
    {
        if (languageName.text == "ENGLISH")
        {
            settingTitle.text = "設定";
            volumeTitle.text = "音量";
            languageTitle.text = "語言";
            languageName.text = "中文";
            settingTitle.font = genFont;
            volumeTitle.font = genFont;
            languageTitle.font = genFont;
            languageName.font = genFont;
        }
        else if (languageName.text == "中文")
        {
            settingTitle.text = "SETTINGS";
            volumeTitle.text = "VOLUME";
            languageTitle.text = "LANGUAGE";
            languageName.text = "ENGLISH";
            settingTitle.font = pixelFont;
            volumeTitle.font = pixelFont;
            languageTitle.font = pixelFont;
            languageName.font = pixelFont;
        }
    }

    public void OnVolumeChanged()
    {
        bgm.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("volume",volumeSlider.value);
    }

}
