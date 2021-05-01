using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class HouseGameManager : MonoBehaviour
{
    public DialogueController dialogueController;
    public Dialogue manD, womenD, sheepD, buildUpD, endD;
    public Animator loadingAni;
    public GameObject shapeAni, messageBox;
    public Text message, debug;
    int count;
    bool isMain, isPailion, isWorkshop, isHouse, isAll;

    public BGMManager bgm;
    public AudioClip endC;
    public GameObject clickAudio;

    [SerializeField]
    ARTrackedImageManager imageManager;
    private void Start()
    {
        loadingAni.SetTrigger("fade");
        messageBox.SetActive(true);
        message.text = "請前往半穴居屋\n並利用相機掃描";
    }
    private void Update()
    {
        if (isAll)
            return;
        else if (count == 4 && !isAll)
        {
            StartCoroutine(BuildUpDialogCtrl());
            isAll = true;
        }
        else if (count < 4)
            ListAllImages();
    }
    void ListAllImages()
    {
        foreach (var trackedImage in imageManager.trackables)
        {
            debug.text = trackedImage.referenceImage.name;
            switch (trackedImage.referenceImage.name)
            {
                case "house":
                    if (!isHouse)
                    {
                        Instantiate(clickAudio);
                        messageBox.SetActive(true);
                        message.text = "請尋找周圍的\nNPC";
                        count++;
                        isHouse = true;
                    }
                    break;
                case "main":
                    if (!isMain)
                    {
                        Instantiate(clickAudio);
                        messageBox.SetActive(false);
                        StartCoroutine(WaitForD(womenD));
                        isMain = true;
                    }
                    break;
                case "workshop":
                    if (!isWorkshop)
                    {
                        Instantiate(clickAudio);
                        messageBox.SetActive(false);
                        StartCoroutine(WaitForD(manD));
                        isWorkshop = true;
                    }
                    break;
                case "pavilion":
                    if (!isPailion)
                    {
                        Instantiate(clickAudio);
                        messageBox.SetActive(false);
                        StartCoroutine(WaitForD(sheepD));
                        isPailion = true;
                    }
                    break;
            }
        }
    }
    IEnumerator WaitForD(Dialogue dialogue)
    {
        dialogueController.StartDialogue(dialogue);
        yield return new WaitUntil(() => dialogueController.isEnd);
        count++;
    }
    IEnumerator BuildUpDialogCtrl()
    {
        dialogueController.StartDialogue(buildUpD);
        yield return new WaitUntil(() => dialogueController.isEnd);

        GenAroundCamera(shapeAni, 0, 0,18);
        GameObject.FindGameObjectWithTag("shapeAni").transform.LookAt(Camera.main.transform);
        yield return new WaitForSeconds(4f);
        bgm.FadeChangeBGM(endC);
        dialogueController.StartDialogue(endD);
        yield return new WaitUntil(() => dialogueController.isEnd);

        SceneManager.LoadScene("Start");
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
        SceneManager.LoadScene("start");
    }
}
