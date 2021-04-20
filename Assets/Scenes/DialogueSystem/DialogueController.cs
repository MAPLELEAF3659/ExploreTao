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

    public float textSpeed;
    public bool isEnd, isFinish = true;

    void Start()
    {
        names = new Queue<string>();
        sentences = new Queue<string>();
        speakers = new Queue<Sprite>();

    }

    void Update()
    {
        if (!isEnd & (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began
               || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            DisplayNext();
        }
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
        if (sentences.Count == 0)
        {
            chatBoxAni.SetBool("toggle", false);
            isEnd = true;
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
        print("[System]Dialogue:" + name + "," + sentence);
        isFinish = false;

        if (speakerSp == null)
            speaker.enabled = false;
        else
        {
            speaker.enabled = true;
            speaker.sprite = speakerSp;
        }

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
