using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginAniGameManager : MonoBehaviour
{
    public DialogueController dialogueController;
    public Dialogue firstD, godD, findFoodD, notGoodD;
    public Animator beginAni, loadingAni;
    public GameObject pot, fire, fish, crab, shell;
    public Text loadingText;

    void Start()
    {
        StartCoroutine(BeginAni());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("start");
        }
    }
    IEnumerator BeginAni()
    {
        yield return new WaitForSeconds(5f);

        dialogueController.StartDialogue(firstD);
        yield return new WaitUntil(() => dialogueController.isEnd);
        beginAni.SetTrigger("next");

        dialogueController.StartDialogue(godD);
        yield return new WaitUntil(() => dialogueController.isEnd);
        beginAni.SetTrigger("next");
        yield return new WaitForSeconds(1.5f);
        beginAni.SetTrigger("next");

        dialogueController.StartDialogue(findFoodD);
        StartCoroutine(PotAni());
        yield return new WaitUntil(() => dialogueController.isEnd);

        loadingText.text = "吃飽了之後...";
        loadingAni.SetTrigger("fade");
        yield return new WaitForSeconds(5f);
        loadingAni.SetTrigger("fade");

        dialogueController.StartDialogue(notGoodD);
        yield return new WaitUntil(() => dialogueController.isEnd);
        loadingText.text = "Loading";
        loadingAni.SetTrigger("fade");
        SceneManager.LoadScene("Boat");
    }

    IEnumerator PotAni()
    {
        GenAroundCamera(pot, 0, -2, 5);
        yield return new WaitForSeconds(1f);
        GenAroundCamera(shell, 0.5f, 0, 3);
        yield return new WaitForSeconds(0.5f);
        GenAroundCamera(fish, 0, 0, 3);
        yield return new WaitForSeconds(0.5f);
        GenAroundCamera(crab, -0.5f, 0, 3);
        yield return new WaitUntil(() => dialogueController.isEnd);
        GameObject[] foods = GameObject.FindGameObjectsWithTag("Food");
        foreach (GameObject temp in foods)
        {
            temp.GetComponent<FoodMove>().Move();
        }
    }

    public void GenAroundCamera(GameObject obj, float x, float y, float z)
    {
        Instantiate(obj, Camera.main.transform.position + Camera.main.transform.right * x
            + Camera.main.transform.up * y + Camera.main.transform.forward * z,
            Camera.main.transform.rotation);
    }
}
