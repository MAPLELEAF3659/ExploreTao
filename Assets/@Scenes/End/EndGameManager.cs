using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{

    public GameObject shapeAni;
    public DialogueController dialogueController;
    public Dialogue endD;
    public Animator loadingAni;
    public GameObject guide;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndD());
        loadingAni.SetTrigger("fade");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator EndD()
    {
        Instantiate(shapeAni,Camera.main.transform.position + Camera.main.transform.forward*18,Camera.main.transform.rotation);
        GameObject.FindGameObjectWithTag("shapeAni").transform.LookAt(Camera.main.transform);
        yield return new WaitForSeconds(4f);
        dialogueController.StartDialogue(endD);
        yield return new WaitUntil(() => dialogueController.isEnd);

        loadingAni.SetTrigger("fade");
        SceneManager.LoadScene("Start");
    }
    public void GenAroundCamera(GameObject obj, float x, float y, float z)
    {
        Instantiate(obj, Camera.main.transform.position + Camera.main.transform.right * x
            + Camera.main.transform.up * y + Camera.main.transform.forward * z,
            Camera.main.transform.rotation);
    }
}
