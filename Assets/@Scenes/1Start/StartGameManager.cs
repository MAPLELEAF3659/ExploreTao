using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameManager : MonoBehaviour
{
    public Text version;
    public Animator loadingAni;
    public GameObject clickAudio;
    public void beginBtnOnClick()
    {
        Instantiate(clickAudio);
        loadingAni.SetTrigger("fade");
        SceneManager.LoadScene("BeginAnimation");
    }
    public void clanBtnOnClick()
    {
        loadingAni.SetTrigger("fade");
        SceneManager.LoadScene("SelectClan");
    }

    public void clothBtnOnClick()
    {
        loadingAni.SetTrigger("fade");
        SceneManager.LoadScene("Cloth");
    }
    // Start is called before the first frame update
    void Start()
    {
        version.text = "Ver " + Application.version;
        PlayerPrefs.SetInt("state",0);
        if (!PlayerPrefs.HasKey("volume"))
            PlayerPrefs.SetFloat("volume",0.5f);
        loadingAni.SetTrigger("fade");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
