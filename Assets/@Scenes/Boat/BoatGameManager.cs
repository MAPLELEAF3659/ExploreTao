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
    public Dialogue introBoatD;
    public Animator loadingAni;
    public ARCameraManager aRCamera;
    public ARFaceManager faceManager;
    public Text msg;
    public GameObject nextBtn,FishFly;
    public BGMManager bgm;
    public GameObject clickAudio;
    //public GameObject video;

    public void ImageOnChanged()
    {
        Instantiate(clickAudio);
        msg.gameObject.SetActive(false);
        StartCoroutine(DialogCtrl());
    }
    public void FaceOnChanged()
    {
        Instantiate(clickAudio);
        msg.gameObject.SetActive(false);
        nextBtn.SetActive(true);
    }

    private void Start()
    {
        msg.text = "�Ыe�����O��\n�åH�۾����y";
        msg.gameObject.SetActive(true);
        nextBtn.SetActive(false);
        loadingAni.SetTrigger("fade");
        //ImageOnChanged();
    }

    private void Update()
    {

    }

    IEnumerator DialogCtrl()
    {
        yield return new WaitForFixedUpdate();
        dialogueController.StartDialogue(introBoatD);
        yield return new WaitUntil(() => dialogueController.isEnd);

        aRCamera.requestedFacingDirection = CameraFacingDirection.User;
        GameObject.FindGameObjectWithTag("fishFlyAni").SetActive(false);
        msg.text = "�бN�۾�����y��\n�Y�i��W\n�F���ڶǲΪA��";
        msg.gameObject.SetActive(true);
    }

    public void Next()
    {
        GenAroundCamera(FishFly,0,0,18);
        GameObject.FindGameObjectWithTag("fishFlyAni").transform.LookAt(Camera.main.transform);
    }

    public void NextBtn()
    {
        Instantiate(clickAudio);
        loadingAni.SetTrigger("fade");
        SceneManager.LoadScene("Fish");
    }
    public void BackBtn()
    {
        Instantiate(clickAudio);
        SceneManager.LoadScene("start");
    }
    public void GenAroundCamera(GameObject obj, float x, float y, float z)
    {
        Instantiate(obj, Camera.main.transform.position + Camera.main.transform.right * x
            + Camera.main.transform.up * y + Camera.main.transform.forward * z,
            Camera.main.transform.rotation);
    }
}
