using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class SelectClanManager : MonoBehaviour
{
    [SerializeField]
    ARTrackedImageManager imageManager;

    public Text message;

    void OnEnable() => imageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => imageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            message.text = "Image Detected!\n" + newImage.name;
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            
            // Handle updated event
        }

        foreach (var removedImage in eventArgs.removed)
        {

            message.text = "Not Detected";
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start");
        }
    }
}
