using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCallBack : MonoBehaviour
{
    GameObject gameManager;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager.BroadcastMessage("ImageOnChanged");
    }

}
