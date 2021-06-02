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
        messageBox.SetActive(true);
        message.text = "�дM��P��\nNPC";
        loadingAni.SetTrigger("fade");
        /*messageBox.SetActive(true);
        message.text = "�Ыe���b�ީ~��\n�çQ�ά۾����y";*/
    }
    private void Update()
    {
        if (isAll)
            return;
        else if (count == 3 && !isAll)
        {
            StartCoroutine(BuildUpDialogCtrl());
            isAll = true;
        }
        else if (count < 3)
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
                        message.text = "�дM��P��\nNPC";
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

        PlayerPrefs.SetInt("state",3);
        SceneManager.LoadScene("Explore");
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
