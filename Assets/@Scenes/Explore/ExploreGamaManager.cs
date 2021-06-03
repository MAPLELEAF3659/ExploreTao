using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExploreGamaManager : MonoBehaviour
{
    public GameObject floor360;
    MeshRenderer storyMesh;
    public Material floor1mat, floor2mat;
    public GameObject floor1, floor2;
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
        storyMesh = floor360.GetComponent<MeshRenderer>();
        state = PlayerPrefs.GetInt("state");
        if (state == 1)//Before Boat Game
        {
            storyMesh.material = floor1mat;
            floor1.SetActive(true);
            floor2.SetActive(false);
            floor360.transform.Rotate(0, 276.78f, 0);
            floor1.transform.Rotate(0, 276.78f, 0);
            msg.text = "請根據指示前往\n1F拼板舟";
        }
        else if (state == 2)//before house game
        {
            storyMesh.material = floor2mat;
            floor1.SetActive(false);
            floor2.SetActive(true);
            floor360.transform.Rotate(0, -21.53f, 0);
            floor2.transform.Rotate(0, -21.53f, 0); 
            msg.text = "請根據指示前往\n2F半穴居屋";
        }
        else if (state == 3)//before end game
        {
            storyMesh.material = floor2mat;
            floor1.SetActive(false);
            floor2.SetActive(true);
            floor360.transform.Rotate(0, 105.73f, 0);
            floor2.transform.Rotate(0, 105.73f, 0);
            msg.text = "請根據指示前往\n2F達悟族宗柱";
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
                floor1.SetActive(false);
                GenAroundCamera(guide, 0, 0, 2);
                StartCoroutine(GuideTalking(boatGuide));

            }
            else if (hit.transform.name == "HousePin")
            {
                floor2.SetActive(false);
                GenAroundCamera(guide, 0, 0, 2);
                StartCoroutine(GuideTalking(houseGuide));

            }
            else if (hit.transform.name == "StickPin")
            {
                floor2.SetActive(false);
                GenAroundCamera(guide, 0, 0, 2);
                StartCoroutine(GuideTalking(stickGuide));

            }
        }
    }

    IEnumerator GuideTalking(Dialogue dialogue)
    {
        isHit = true;
        yield return new WaitForFixedUpdate();
        GameObject.FindGameObjectWithTag("guide").transform.LookAt(Camera.main.transform.position);
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
