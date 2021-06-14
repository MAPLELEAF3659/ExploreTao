using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    private Queue<string> sentences, names;
    private Queue<Sprite> speakers;
    public Animator chatBoxAni;
    public Text cText, cName;
    public Image speaker;
    public GameObject clickAudio;

    public float textSpeed;
    public bool isEnd = true, isFinish = true;

    public GameObject gameManager;
    public GameObject guide, bird;

    void Start()
    {
        names = new Queue<string>();
        sentences = new Queue<string>();
        speakers = new Queue<Sprite>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    public void StartDialogue(Dialogue dialogue)
    {
        print("[System]Dialogue is begin");
        isEnd = false;
        names.Clear();
        sentences.Clear();
        speakers.Clear();
        foreach (string name in dialogue.name)
        {
            names.Enqueue(name);
        }
        foreach (string sentence in dialogue.sentence)
        {
            sentences.Enqueue(sentence);
        }
        foreach (Sprite speaker in dialogue.speaker)
        {
            speakers.Enqueue(speaker);
        }
        chatBoxAni.SetBool("toggle", true);
        DisplayNext();
    }

    public void DisplayNext()
    {
        Instantiate(clickAudio);
        if (sentences.Count == 0)
        {
            chatBoxAni.SetBool("toggle", false);
            isEnd = true;
            print("[System]Dialogue is end");
            return;
        }
        else if (!isFinish)
        {
            isFinish = true;
        }
        else if (isFinish)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(names.Dequeue(), sentences.Dequeue(), speakers.Dequeue()));
        }
    }
    IEnumerator TypeSentence(string name, string sentence, Sprite speakerSp)
    {
        print("[System]Dialogue - " + name + ":" + sentence);
        isFinish = false;

        if (speakerSp == null)
            speaker.enabled = false;
        else
        {
            speaker.enabled = true;
            speaker.sprite = speakerSp;
        }

        if (name.Contains("*"))
        {
            cName.text = name.Substring(0, name.IndexOf("*"));
            switch (name.Substring(name.IndexOf("*"), 6))
            {
                case "*NEXT*":
                    {
                        gameManager.SendMessage("Next");
                        print("Boardcast NEXT in " + gameManager.name);
                    }
                    break;
                case "*TALK*":
                    if (GameObject.FindGameObjectWithTag("talkingNPC"))
                        GameObject.FindGameObjectWithTag("talkingNPC").GetComponent<Animator>().SetTrigger("talking");
                    break;
                case "*GODS*":
                    GameObject.FindGameObjectWithTag("gods").GetComponent<Text>().text = sentence;
                    isFinish = true;
                    yield break;
                case "*GUID*":
                    if (GameObject.FindGameObjectWithTag("talkingNPC"))
                        GameObject.Destroy(GameObject.FindGameObjectWithTag("talkingNPC"));
                    Instantiate(guide, Camera.main.transform.position, Camera.main.transform.rotation);
                    break;
                case "*BIRD*":
                    if (GameObject.FindGameObjectWithTag("talkingNPC"))
                        GameObject.Destroy(GameObject.FindGameObjectWithTag("talkingNPC"));
                    Instantiate(bird, Camera.main.transform.position, Camera.main.transform.rotation);
                    break;
                case "*DSRY*":
                    if (GameObject.FindGameObjectWithTag("talkingNPC"))
                        GameObject.Destroy(GameObject.FindGameObjectWithTag("talkingNPC"));
                    break;
            }
        }
        else
            cName.text = name;
        cText.text = null;

        foreach (char letter in sentence.ToCharArray())
        {
            if (isFinish)
            {
                cText.text = sentence;
                yield break;
            }
            cText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        isFinish = true;
    }
}
