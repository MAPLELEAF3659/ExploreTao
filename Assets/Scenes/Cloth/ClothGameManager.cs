using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ClothGameManager : MonoBehaviour
{
    public Text message;
    [SerializeField]
    ARFaceManager faceManager;

    void OnEnable() => faceManager.facesChanged += OnChanged;

    void OnDisable() => faceManager.facesChanged -= OnChanged;

    void OnChanged(ARFacesChangedEventArgs eventArgs)
    {
        foreach (var newFace in eventArgs.added)
        {
            message.text = "Face Detected!\n" + newFace.name;
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
