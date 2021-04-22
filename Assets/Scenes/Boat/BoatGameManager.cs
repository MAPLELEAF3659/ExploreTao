using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class BoatGameManager : MonoBehaviour
{
    public DialogueController dialogueController;
    public Dialogue introBoatD, introFishD;
    public Animator loadingAni;

    public ARTrackedImageManager imageManager;
    void OnEnable() => imageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => imageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        StartCoroutine(DialogCtrl());
    }

    IEnumerator DialogCtrl()
    {
        dialogueController.StartDialogue(introBoatD);
        yield return new WaitUntil(() => dialogueController.isEnd);
        dialogueController.StartDialogue(introFishD);
        yield return new WaitUntil(() => dialogueController.isEnd);
    }

    public void Next()
    {

    }
}
