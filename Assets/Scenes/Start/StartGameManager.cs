using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameManager : MonoBehaviour
{
    public Text version;

    public void beginBtnOnClick()
    {
        SceneManager.LoadScene("BeginAnimation");
    }
    public void clanBtnOnClick()
    {
        SceneManager.LoadScene("SelectClan");
    }

    public void clothBtnOnClick()
    {
        SceneManager.LoadScene("Cloth");
    }
    // Start is called before the first frame update
    void Start()
    {
        version.text = "Ver " + Application.version;
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
