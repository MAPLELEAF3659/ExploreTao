using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spearController : MonoBehaviour
{
    public BirdGameManager gameManager;
    public float speed = 0.1f, time = 3f;
    public GameObject hitEffect;
    public GameObject hitAudio;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BirdGameManager>();
        transform.rotation = Camera.main.transform.rotation;
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*200);
    }

    void Update()
    {
        if (time <= 0)
            GameObject.Destroy(gameObject);
        else
            time -= Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bird")
        {
            print("Hit the bird!");
            gameManager.birdHp -= 1;
            Instantiate(hitEffect,transform.position,transform.rotation);
            Instantiate(hitAudio);
            GameObject.Destroy(gameObject);
        }
    }
}
