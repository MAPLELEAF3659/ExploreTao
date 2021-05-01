using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothCallBack : MonoBehaviour
{
    GameObject gameManager;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager.BroadcastMessage("FaceOnChanged");
    }
}
