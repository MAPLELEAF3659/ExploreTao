using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FishGameManager : MonoBehaviour
{
    public DialogueController dialogueController;
    public Dialogue gameStartD, endD;
    RaycastHit hit;
    public GameObject scoreBoard, bucket, genPointL, genPointR, fishHitAudio;
    public GameObject[] fish;
    public Text scoreText;
    int score;
    public bool isPlaying;
    public Animator loadingAni;
    public BGMManager bgm;
    public AudioClip winC;
    public GameObject clickAudio;
    // Start is called before the first frame update
    void Start()
    {
        loadingAni.SetTrigger("fade");
        scoreBoard.SetActive(false);
        bucket.SetActive(false);
        StartCoroutine(GameIntroCtrl());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if ((Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began))
            {
                CheckRayHit(Input.GetTouch(0).position);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CheckRayHit(Input.mousePosition);
            }
            if (score >= 100)
            {
                isPlaying = false;
                StartCoroutine(EndDCtrl());
            }
        }
    }
    void CheckRayHit(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out hit))
        {
            print(hit.transform.name);
            if (hit.transform.tag == "purpleFish")
            {
                Instantiate(fishHitAudio);
                hit.transform.gameObject.GetComponent<FishContrller>().StartCoroutine("FlyToBucket");
                score += 10;
                scoreText.text = score.ToString("000") + "¤À";
            }
            else if (hit.transform.tag == "dotFish")
            {
                Instantiate(fishHitAudio);
                hit.transform.gameObject.GetComponent<FishContrller>().StartCoroutine("FlyToBucket");
                score += 25;
                scoreText.text = score.ToString("000") + "¤À";
            }
            else if (hit.transform.tag == "whiteFish")
            {
                Instantiate(fishHitAudio);
                hit.transform.gameObject.GetComponent<FishContrller>().StartCoroutine("FlyToBucket");
                score += 50;
                scoreText.text = score.ToString("000") + "¤À";
            }
        }
    }
    public IEnumerator GameIntroCtrl()
    {
        yield return new WaitForEndOfFrame();
        dialogueController.StartDialogue(gameStartD);
        yield return new WaitUntil(() => dialogueController.isEnd);
        scoreBoard.SetActive(true);
        bucket.SetActive(true);
        GenAroundCamera(genPointL, 4, -2, 8);
        GenAroundCamera(genPointR, -4, -2, 8);
        isPlaying = true;
        StartCoroutine(RandomGenFish());
    }

    public IEnumerator EndDCtrl()
    {
        isPlaying = false;
        bgm.FadeChangeBGM(winC);
        dialogueController.StartDialogue(endD);
        yield return new WaitUntil(() => dialogueController.isEnd);
        SceneManager.LoadScene("Bird");
    }

    public IEnumerator RandomGenFish()
    {
        while (isPlaying)
        {
            Instantiate(fish[Random.Range(0, fish.Length)]);
            yield return new WaitForSeconds(Random.Range(0.5f, 2));
        }
    }

    public void GenAroundCamera(GameObject obj, float x, float y, float z)
    {
        Instantiate(obj, Camera.main.transform.position + Camera.main.transform.right * x
            + Camera.main.transform.up * y + Camera.main.transform.forward * z,
            Camera.main.transform.rotation);
    }
    public void BackBtn()
    {
        Instantiate(clickAudio);
        SceneManager.LoadScene("Start");
    }
}
