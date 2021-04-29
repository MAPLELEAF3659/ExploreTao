using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;

public class BoatGameManager : MonoBehaviour
{
    public DialogueController dialogueController;
    public Dialogue introBoatD, gameIntroD;
    public Animator loadingAni;
    public ARCameraManager aRCamera;
    public Text msg;
    public VideoPlayer video;
    bool isStart, isClothOn;

    public ARTrackedImageManager imageManager;
    public ARFaceManager faceManager;
    void OnEnable()
    {
        imageManager.trackedImagesChanged += ImageOnChanged;
        faceManager.facesChanged += FaceOnChanged;
    }
    void OnDisable()
    {
        imageManager.trackedImagesChanged -= ImageOnChanged;
        faceManager.facesChanged -= FaceOnChanged;
    }

    void ImageOnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        if (!isStart)
        {
            msg.gameObject.SetActive(false);
            StartCoroutine(DialogCtrl());
            isStart = true;
        }
    }
    void FaceOnChanged(ARFacesChangedEventArgs eventArgs)
    {
        if (!isClothOn)
        {
            StartCoroutine(GameDCtrl());
            isClothOn = true;
        }
    }

    private void Start()
    {
        msg.text = "�Ыe�����O��\n�åH�۾����y";
        msg.gameObject.SetActive(true);
        loadingAni.SetTrigger("fade");
    }

    private void Update()
    {

    }

    IEnumerator DialogCtrl()
    {
        dialogueController.StartDialogue(introBoatD);
        yield return new WaitUntil(() => dialogueController.isEnd);

        aRCamera.requestedFacingDirection = CameraFacingDirection.User;
        msg.text = "�n��W�F���ǲΪA��\n�бN�۾�����y��";
        msg.gameObject.SetActive(true);
    }
    IEnumerator GameDCtrl()
    {
        msg.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        dialogueController.StartDialogue(gameIntroD);
        yield return new WaitUntil(() => dialogueController.isEnd);
        video.Play();
    }
    public void Next()
    {

    }
    public void BackBtn()
    {
        SceneManager.LoadScene("start");
    }
}
