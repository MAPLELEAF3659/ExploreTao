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
    public GameObject nextBtn, FishFly, pFish, dFish, wFish;
    public BGMManager bgm;
    public GameObject clickAudio;
    //public GameObject video;

    public void FaceOnChanged()
    {
        Instantiate(clickAudio);
        msg.gameObject.SetActive(false);
        nextBtn.SetActive(true);
    }

    private void Start()
    {
        //msg.text = "�Ыe�����O��\n�åH�۾����y";
        msg.gameObject.SetActive(false);
        nextBtn.SetActive(false);

        StartCoroutine(DialogCtrl());
        loadingAni.SetTrigger("fade");
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

    int n = 1;
    public void Next()
    {
        switch (n)
        {
            case 1:
                GameObject.Destroy(GameObject.FindGameObjectWithTag("talkingNPC"));
                GenAroundCamera(wFish, 0, 0, 3, false);
                break;
            case 2:
                GameObject.Destroy(GameObject.FindGameObjectWithTag("whiteFish"));
                GenAroundCamera(dFish, 0, 0, 3, false);
                break;
            case 3:
                GameObject.Destroy(GameObject.FindGameObjectWithTag("dotFish"));
                GenAroundCamera(pFish, 0, 0, 3, false);
                break;
            case 4:
                GameObject.Destroy(GameObject.FindGameObjectWithTag("purpleFish"));
                GenAroundCamera(FishFly, 0, 0, 18, true);
                GameObject.FindGameObjectWithTag("fishFlyAni").transform.LookAt(Camera.main.transform);
                break;
            default:
                return;
        }
        n++;
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
    public void GenAroundCamera(GameObject obj, float x, float y, float z, bool faceToCam)
    {
        if (faceToCam)
            Instantiate(obj, Camera.main.transform.position + Camera.main.transform.right * x
                + Camera.main.transform.up * y + Camera.main.transform.forward * z,
                Camera.main.transform.rotation);
        else
            Instantiate(obj, Camera.main.transform.position + Camera.main.transform.right * x
                + Camera.main.transform.up * y + Camera.main.transform.forward * z, obj.transform.rotation);

    }
}
