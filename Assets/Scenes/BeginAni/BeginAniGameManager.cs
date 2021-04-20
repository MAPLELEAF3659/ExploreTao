using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginAniGameManager : MonoBehaviour
{
    public DialogueController dialogueController;
    public Dialogue firstD, godD;
    public Animator beginAni;

    void Start()
    {
        StartCoroutine(AniControl());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("start");
        }
    }
    IEnumerator AniControl()
    {
        yield return new WaitForSeconds(5f);
        dialogueController.StartDialogue(firstD);
        yield return new WaitUntil(() => dialogueController.isEnd == true);
        beginAni.SetTrigger("godDown");
        dialogueController.StartDialogue(godD);
    }
}
