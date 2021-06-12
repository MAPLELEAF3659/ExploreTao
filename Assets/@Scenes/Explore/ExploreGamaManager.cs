using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExploreGamaManager : MonoBehaviour
{
    MeshRenderer storyMesh;
    public GameObject floor1, floor2_1,floor2_2;
    public Text msg;
    public GameObject msgBox;
    int state;
    public Animator loadingAni;
    public GameObject guide;
    public Dialogue boatGuide, houseGuide, stickGuide;
    public DialogueController dialogueController;

    // Start is called before the first frame update
    void Start()
    {
        state = PlayerPrefs.GetInt("state");
        if (state == 1)//Before Boat Game
        {
            floor1.SetActive(true);
            floor2_1.SetActive(false);
            floor2_2.SetActive(false);
            msg.text = "�Юھګ��ܫe��\n1F���O��";
        }
        else if (state == 2)//before house game
        {
            floor1.SetActive(false);
            floor2_1.SetActive(true);
            floor2_2.SetActive(false);
            msg.text = "�Юھګ��ܫe��\n2F�b�ީ~��";
        }
        else if (state == 3)//before end game
        {
            floor1.SetActive(false);
            floor2_1.SetActive(false);
            floor2_2.SetActive(true);
            msg.text = "�Юھګ��ܫe��\n2F�F���کv�W";
        }
        else
        {
            SceneManager.LoadScene("start");
        }
        msgBox.SetActive(true);
        loadingAni.SetTrigger("fade");
    }

    RaycastHit hit;
    bool isHit;
    // Update is called once per frame
    void Update()
    {
        if (!isHit)
        {
            if ((Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began))
            {
                CheckRayHit(Input.GetTouch(0).position);
                msgBox.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CheckRayHit(Input.mousePosition);
                msgBox.SetActive(false);
            }
        }
    }

    void CheckRayHit(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out hit))
        {
            print(hit.transform.name);
            if (hit.transform.name == "BoatPin")
            {
                //floor1.SetActive(false);
                //GenAroundCamera(guide, 0, 0, 2);
                StartCoroutine(GuideTalking(boatGuide));
            }
            else if (hit.transform.name == "HousePin")
            {
                //floor2_1.SetActive(false);
                //GenAroundCamera(guide, 0, 0, 2);
                StartCoroutine(GuideTalking(houseGuide));
            }
            else if (hit.transform.name == "StickPin")
            {
                //floor2_2.SetActive(false);
                //GenAroundCamera(guide, 0, 0, 2);
                StartCoroutine(GuideTalking(stickGuide));
            }
        }
    }

    IEnumerator GuideTalking(Dialogue dialogue)
    {
        isHit = true;
        yield return new WaitForFixedUpdate();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("sign"))
            GameObject.Destroy(obj);
        dialogueController.StartDialogue(dialogue);
        yield return new WaitUntil(() => dialogueController.isEnd);

        switch (state)
        {
            case 1:
                SceneManager.LoadScene("Boat");
                break;
            case 2:
                SceneManager.LoadScene("House");
                break;
            case 3:
                SceneManager.LoadScene("End");
                break;
        }
    }

    public void GenAroundCamera(GameObject obj, float x, float y, float z)
    {
        Instantiate(obj, Camera.main.transform.position + Camera.main.transform.right * x
            + Camera.main.transform.up * y + Camera.main.transform.forward * z,
            Camera.main.transform.rotation);
    }
}
