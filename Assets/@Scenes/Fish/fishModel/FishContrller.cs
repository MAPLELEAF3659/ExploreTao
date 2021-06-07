using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishContrller : MonoBehaviour
{
    public FishGameManager gameManager;
    public GameObject expParticle,waterParticle;
    bool isMoving = true;

    public List<GameObject> startPoint;
    public GameObject target;

    float a, b, c;
    float cos, sin;
    public float forceX, forceY, forceZ;
    public float maxTime;
    float time;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FishGameManager>();
        startPoint.Add(GameObject.FindGameObjectWithTag("FishGenPointLeft"));
        startPoint.Add(GameObject.FindGameObjectWithTag("FishGenPointRight"));
        GameObject chooseStartPoint = startPoint[Random.Range(0, 2)];
        transform.position = chooseStartPoint.transform.position;
        switch (chooseStartPoint.tag)
        {
            case "FishGenPointRight":
                target = GameObject.FindGameObjectWithTag("FishGenPointLeft");
                transform.Rotate(new Vector3(0,90,0));
                break;
            case "FishGenPointLeft":
                target = GameObject.FindGameObjectWithTag("FishGenPointRight");
                transform.Rotate(new Vector3(0, -90, 0));
                break;
        }

        a = target.transform.position.x - transform.position.x;
        b = target.transform.position.z - transform.position.z;
        c = Mathf.Pow(a * a + b * b, 0.5f);
        cos = a / c;
        sin = b / c;
        GetComponent<Rigidbody>().velocity = new Vector3(forceX * cos, forceY, forceZ * sin);
        Instantiate(waterParticle, chooseStartPoint.transform);
    }

    void Update()
    {
        //transform.LookAt(Camera.main.transform);
        if ((!gameManager.isPlaying && isMoving) || (time > maxTime))
        {
            StartCoroutine(Disapper());
            isMoving = false;
        }
        else
            time += Time.deltaTime;
    }
    IEnumerator Disapper()
    {
        Instantiate(expParticle, transform);
        yield return new WaitForSeconds(0.7f);
        GameObject.Destroy(gameObject);
    }
    public IEnumerator FlyToBucket()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        Instantiate(expParticle, transform);
        while (Vector3.Distance(transform.position, Camera.main.transform.position) > 1f)
        {
            transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position, 0.1f);
            yield return new WaitForFixedUpdate();
        }
        GameObject.Destroy(gameObject);
    }
}
