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
    //public GameObject video;

    public void ImageOnChanged()
    {
        msg.gameObject.SetActive(false);
        StartCoroutine(DialogCtrl());
    }
    public void FaceOnChanged()
    {
        msg.gameObject.SetActive(false);
        nextBtn.SetActive(true);
    }

    private void Start()
    {
        msg.text = "請前往拼板舟\n並以相機掃描";
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
        msg.text = "請將相機對準臉部\n即可穿上\n達悟族傳統服飾";
        msg.gameObject.SetActive(true);
    }

    public void Next()
    {
        GenAroundCamera(FishFly,0,0,18);
        GameObject.FindGameObjectWithTag("fishFlyAni").transform.LookAt(Camera.main.transform);
    }

    public void NextBtn()
    {
        loadingAni.SetTrigger("fade");
        SceneManager.LoadScene("Fish");
    }
    public void BackBtn()
    {
        SceneManager.LoadScene("start");
    }
    public void GenAroundCamera(GameObject obj, float x, float y, float z)
    {
        Instantiate(obj, Camera.main.transform.position + Camera.main.transform.right * x
            + Camera.main.transform.up * y + Camera.main.transform.forward * z,
            Camera.main.transform.rotation);
    }
}
