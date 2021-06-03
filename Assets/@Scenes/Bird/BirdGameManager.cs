using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BirdGameManager : MonoBehaviour
{
    public GameObject bird, fishRack, spear;
    public DialogueController dialogueController;
    public Dialogue startD, winD, loseD;
    public Animator loadingAni;
    public bool isPlaying;
    public float birdHp = 10, time = 100, cd = 1f;
    public Text birdHpText, timeText, cdText;
    public GameObject scoreBoard;
    public GameObject spearStartPos;

    // Start is called before the first frame update
    void Start()
    {
        scoreBoard.SetActive(false);
        loadingAni.SetTrigger("fade");
        StartCoroutine(startDCtrl());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if ((((Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began))
                || Input.GetKeyDown(KeyCode.Mouse0)) && cd <= 0)
            {
                GenAroundCenter(spear, spearStartPos, new Vector3(0, 0, 0));
                cd = 1f;
            }
            if (birdHp <= 0)
            {
                birdHpText.text = "�Q�~��q�G0/10";
                StopCoroutine(Timer());
                isPlaying = false;
                StartCoroutine(winDCtrl());
            }
            else
                birdHpText.text = "�Q�~��q�G" + birdHp + "/10";
            if (cd > 0)
                cd -= Time.deltaTime;
            else
                cd = 0;
            cdText.text = "CD:" + cd.ToString("0.0") + "s";
            if (time <= 0)
            {
                isPlaying = false;
                StartCoroutine(loseDCtrl());
            }

        }
    }

    public IEnumerator startDCtrl()
    {
        yield return new WaitForFixedUpdate();
        dialogueController.StartDialogue(startD);
        yield return new WaitUntil(() => dialogueController.isEnd);

        GenAroundCenter(bird, Camera.main.gameObject, new Vector3(0, 0, 10));
        GenAroundCenter(fishRack, Camera.main.gameObject, new Vector3(0, -5, 15));
        scoreBoard.SetActive(true);
        isPlaying = true;
        StartCoroutine(Timer());
    }

    public IEnumerator loseDCtrl()
    {
        yield return new WaitForFixedUpdate();
        dialogueController.StartDialogue(loseD);
        yield return new WaitUntil(() => dialogueController.isEnd);

        SceneManager.LoadScene("Bird");
    }

    public IEnumerator winDCtrl()
    {
        yield return new WaitForFixedUpdate();
        GameObject.Destroy(GameObject.FindGameObjectWithTag("bird"));
        dialogueController.StartDialogue(winD);
        yield return new WaitUntil(() => dialogueController.isEnd);
        PlayerPrefs.SetInt("state", 2);
        SceneManager.LoadScene("Explore");
    }
    public IEnumerator Timer()
    {
        yield return new WaitForFixedUpdate();
        while (time > 0)
        {
            time -= Time.deltaTime;
            timeText.text = "�ɶ�:" + time.ToString("000.0") + "��";
            yield return new WaitForFixedUpdate();
        }
        time = 0;
        timeText.text = "�ɶ�:" + time.ToString("000.0") + "��";
    }
    public void GenAroundCenter(GameObject obj, GameObject center, Vector3 posOffset)
    {
        float x = posOffset.x;
        float y = posOffset.y;
        float z = posOffset.z;
        Instantiate(obj, center.transform.position + center.transform.right * x
            + center.transform.up * y + center.transform.forward * z,
            obj.transform.rotation);
    }
}
